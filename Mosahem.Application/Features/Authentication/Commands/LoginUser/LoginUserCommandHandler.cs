using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.DTOs.Auth;
using Mosahem.Application.Interfaces.Security;
using Mosahem.Domain.Entities.Identity;

namespace mosahem.Application.Features.Authentication.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<AuthResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public LoginUserCommandHandler(
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<AuthResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var generalErrorMsg = _localizer[SharedResourcesKeys.General.OperationFailed].Value;
            var detailedErrorMsg = _localizer[SharedResourcesKeys.Auth.InvalidCredentials].Value;

            Dictionary<string, List<string>> GetErrorDict()
            {
                return new Dictionary<string, List<string>>
                {
                    { "Credentials", new List<string> { detailedErrorMsg } }
                };
            }

            var user = await _unitOfWork.Users.GetTableNoTracking()
                .FirstOrDefaultAsync(u => u.Email == request.EmailOrPhone || u.PhoneNumber == request.EmailOrPhone, cancellationToken);

            if (user == null)
                return _responseHandler.BadRequest<AuthResponse>(generalErrorMsg, GetErrorDict());

            if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
                return _responseHandler.BadRequest<AuthResponse>(generalErrorMsg, GetErrorDict());

            var jwtResult = _jwtTokenGenerator.GenerateTokens(user);

            var refreshToken = new RefreshToken
            {
                Token = jwtResult.RefreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                CreatedByIp = "N/A"
            };

            await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken, cancellationToken);
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

            return _responseHandler.Success(response, _localizer[SharedResourcesKeys.Auth.LoginSuccess]);
        }
    }
}