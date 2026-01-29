using MapsterMapper; 
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Identity;
using mosahem.Domain.Entities.Location;
using mosahem.Domain.Entities.Profiles;
using Mosahem.Application.DTOs.Auth;
using Mosahem.Application.Interfaces.Security;
using Mosahem.Domain.Entities.Identity;

namespace mosahem.Application.Features.Authentication.Commands.CompleteOrganizationRegistration
{
    public class CompleteOrganizationRegistrationCommandHandler : IRequestHandler<CompleteOrganizationRegistrationCommand, Response<AuthResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ResponseHandler _responseHandler;
        private readonly IMapper _mapper; 

        public CompleteOrganizationRegistrationCommandHandler(
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

        public async Task<Response<AuthResponse>> Handle(CompleteOrganizationRegistrationCommand request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.Users.IsEmailUniqueAsync(request.Email))
                return _responseHandler.BadRequest<AuthResponse>(_localizer[SharedResourcesKeys.User.EmailAlreadyTaken]);

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var userId = Guid.NewGuid();

                var user = _mapper.Map<MosahmUser>(request);
                user.Id = userId;
                user.PasswordHash = _passwordHasher.HashPassword(request.Password);

                await _unitOfWork.Users.AddAsync(user, cancellationToken);

                var organization = _mapper.Map<Organization>(request);
                organization.Id = userId; 

                await _unitOfWork.Organizations.AddAsync(organization, cancellationToken);

                if (request.Locations != null && request.Locations.Any())
                {
                    var addresses = _mapper.Map<List<Address>>(request.Locations);
                    addresses.ForEach(a => a.OrganizationId = userId);

                    await _unitOfWork.Repository<Address>().AddRangeAsync(addresses, cancellationToken);
                }

                if (request.FieldIds != null && request.FieldIds.Any())
                {
                    var orgFields = _mapper.Map<List<OrganizationField>>(request.FieldIds.Distinct());
                    orgFields.ForEach(f => f.OrganizationId = userId);

                    await _unitOfWork.Repository<OrganizationField>().AddRangeAsync(orgFields, cancellationToken);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                var jwtResult = _jwtTokenGenerator.GenerateTokens(user);

                var refreshTokenEntity = new RefreshToken
                {
                    Token = jwtResult.RefreshToken,
                    ExpiresAt = DateTime.UtcNow.AddDays(7),
                    UserId = user.Id,
                    IsRevoked = false,
                };

                await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshTokenEntity, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                
                var response = new AuthResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = user.Role.ToString(),
                    IsVerified = false,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken,
                    AccessTokenExpiration = jwtResult.ExpireAt
                };

                return _responseHandler.Created(response);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _responseHandler.BadRequest<AuthResponse>($"{_localizer[SharedResourcesKeys.General.OperationFailed]}: {ex.Message}");
            }
        }
    }
}