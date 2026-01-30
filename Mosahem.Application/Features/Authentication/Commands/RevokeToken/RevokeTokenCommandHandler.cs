using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Domain.Entities.Identity;

namespace mosahem.Application.Features.Authentication.Commands.RevokeToken
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public RevokeTokenCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<bool>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _unitOfWork.Repository<RefreshToken>().GetTableAsTracking()
                .FirstOrDefaultAsync(t => t.Token == request.Token, cancellationToken);

            if (refreshToken == null)
                return _responseHandler.BadRequest<bool>(_localizer[SharedResourcesKeys.Auth.InvalidToken]);

            if (refreshToken.IsRevoked)
                return _responseHandler.Success(true, _localizer[SharedResourcesKeys.Auth.LogoutSuccess]);

            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;

            await _unitOfWork.Repository<RefreshToken>().UpdateAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success(true, _localizer[SharedResourcesKeys.Auth.LogoutSuccess]);
        }
    }
}