using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.DTOs.Auth;
using Mosahem.Application.Features.Authentication.Commands.RefreshTokens;
using Mosahem.Application.Interfaces.Security;
using Mosahem.Domain.Entities.Identity;

namespace mosahem.Application.Features.Authentication.Commands.RefreshTokens
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Response<AuthResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public RefreshTokenCommandHandler(
            IUnitOfWork unitOfWork,
            IJwtTokenGenerator jwtTokenGenerator,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var storedToken = await _unitOfWork.Repository<RefreshToken>()
                .GetTableNoTracking()
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

            if (storedToken == null || storedToken.IsRevoked || storedToken.IsExpired)
                return _responseHandler.BadRequest<AuthResponse>(_localizer[SharedResourcesKeys.Validation.Invalid]);

            var user = storedToken.User;

            storedToken.IsRevoked = true;
            storedToken.RevokedAt = DateTime.UtcNow;
            await _unitOfWork.Repository<RefreshToken>().UpdateAsync(storedToken);

            var jwtResult = _jwtTokenGenerator.GenerateTokens(user);

            var newRefreshToken = new RefreshToken
            {
                Token = jwtResult.RefreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
            };

            await _unitOfWork.Repository<RefreshToken>().AddAsync(newRefreshToken, cancellationToken);
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

            return _responseHandler.Success(response);
        }
    }
}