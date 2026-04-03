using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Identity;
using mosahem.Domain.Entities.Location;
using mosahem.Domain.Entities.Profiles;
using Mosahem.Application.DTOs.Auth;
using Mosahem.Application.Features.Authentication.Commands.CompleteVolunteerRegistration;
using Mosahem.Application.Interfaces.Security;
using Mosahem.Domain.Entities;
using Mosahem.Domain.Entities.Identity;

namespace mosahem.Application.Features.Authentication.Commands.CompleteVolunteerRegistration
{
    public class CompleteVolunteerRegistrationCommandHandler : IRequestHandler<CompleteVolunteerRegistrationCommand, Response<AuthResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ResponseHandler _responseHandler;
        private readonly IMapper _mapper;

        public CompleteVolunteerRegistrationCommandHandler(
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            IStringLocalizer<SharedResources> localizer,
            ResponseHandler responseHandler,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _localizer = localizer;
            _responseHandler = responseHandler;
            _mapper = mapper;
        }

        public async Task<Response<AuthResponse>> Handle(CompleteVolunteerRegistrationCommand request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.Users.IsEmailUniqueAsync(request.Email))
            {
                return _responseHandler.BadRequest<AuthResponse>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    BuildError("Email", _localizer[SharedResourcesKeys.User.EmailAlreadyTaken]));
            }

            var transactionStarted = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            if (!transactionStarted)
            {
                return _responseHandler.BadRequest<AuthResponse>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    BuildError("Database.Transaction", _localizer[SharedResourcesKeys.System.TransactionStartFailed]));
            }

            try
            {
                var userId = Guid.NewGuid();

                var user = await AddUserAsync(request, userId, cancellationToken);
                await AddVolunteerProfileAsync(request, userId, cancellationToken);
                await AddOptionalAddressAsync(request, userId, cancellationToken);
                await AddVolunteerFieldsAsync(request, userId, cancellationToken);
                await AddVolunteerSkillsAsync(request, userId, cancellationToken);
                await CleanupTemporaryCvFileAsync(request.CvUrl, cancellationToken);

                var affectedRows = await _unitOfWork.SaveChangesAsync(cancellationToken);
                if (affectedRows <= 0)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return _responseHandler.BadRequest<AuthResponse>(
                        _localizer[SharedResourcesKeys.General.OperationFailed],
                        BuildError("Database.SaveChanges", _localizer[SharedResourcesKeys.System.NoRowsAffected]));
                }

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                var authResponse = await GenerateAuthResponseAsync(user, cancellationToken);
                return _responseHandler.Created(authResponse);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _responseHandler.BadRequest<AuthResponse>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    BuildError("Operation", ex.Message));
            }
        }

        private async Task<MosahmUser> AddUserAsync(CompleteVolunteerRegistrationCommand request, Guid userId, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<MosahmUser>(request);
            user.Id = userId;
            user.PasswordHash = _passwordHasher.HashPassword(request.Password);

            await _unitOfWork.Users.AddAsync(user, cancellationToken);
            return user;
        }

        private async Task AddVolunteerProfileAsync(CompleteVolunteerRegistrationCommand request, Guid userId, CancellationToken cancellationToken)
        {
            var volunteer = _mapper.Map<Volunteer>(request);
            volunteer.Id = userId;

            await _unitOfWork.Volunteers.AddAsync(volunteer, cancellationToken);
        }

        private async Task AddOptionalAddressAsync(CompleteVolunteerRegistrationCommand request, Guid userId, CancellationToken cancellationToken)
        {
            if (request.Location is null)
                return;

            var address = _mapper.Map<Address>(request.Location);
            address.VolunteerId = userId;

            await _unitOfWork.Addresses.AddAsync(address, cancellationToken);
        }

        private async Task AddVolunteerFieldsAsync(CompleteVolunteerRegistrationCommand request, Guid userId, CancellationToken cancellationToken)
        {
            if (request.FieldIds is null || !request.FieldIds.Any())
                return;

            var volunteerFields = _mapper.Map<List<VolunteerField>>(request.FieldIds.Distinct());
            volunteerFields.ForEach(field => field.VolunteerId = userId);

            await _unitOfWork.Repository<VolunteerField>().AddRangeAsync(volunteerFields, cancellationToken);
        }

        private async Task AddVolunteerSkillsAsync(CompleteVolunteerRegistrationCommand request, Guid userId, CancellationToken cancellationToken)
        {
            if (request.SkillIds is null || !request.SkillIds.Any())
                return;

            var volunteerSkills = _mapper.Map<List<VolunteerSkill>>(request.SkillIds.Distinct());
            volunteerSkills.ForEach(skill => skill.VolunteerId = userId);

            await _unitOfWork.Repository<VolunteerSkill>().AddRangeAsync(volunteerSkills, cancellationToken);
        }

        private async Task CleanupTemporaryCvFileAsync(string? cvKey, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(cvKey))
                return;

            var temporaryFiles = await _unitOfWork.Repository<TemporaryFileUpload>()
                .GetTableAsTracking()
                .Where(file => file.FileKey == cvKey)
                .ToListAsync(cancellationToken);

            if (!temporaryFiles.Any())
                return;

            await _unitOfWork.Repository<TemporaryFileUpload>()
                .DeleteRangeAsync(temporaryFiles, cancellationToken);
        }

        private async Task<AuthResponse> GenerateAuthResponseAsync(MosahmUser user, CancellationToken cancellationToken)
        {
            var jwtResult = _jwtTokenGenerator.GenerateTokens(user);

            var refreshTokenEntity = new RefreshToken
            {
                Token = jwtResult.RefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                UserId = user.Id,
                IsRevoked = false
            };

            await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshTokenEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthResponse
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Role = user.Role.ToString(),
                IsVerified = null,
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken,
                AccessTokenExpiration = jwtResult.ExpireAt
            };
        }

        private static Dictionary<string, List<string>> BuildError(string key, string message)
        {
            return new Dictionary<string, List<string>>
            {
                { key, new List<string> { message } }
            };
        }
    }
}
