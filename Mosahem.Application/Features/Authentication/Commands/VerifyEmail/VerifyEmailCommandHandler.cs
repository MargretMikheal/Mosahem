using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Domain.Entities.Identity;
using Mosahem.Domain.Enums;

namespace mosahem.Application.Features.Authentication.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public VerifyEmailCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var otp = await _unitOfWork.Repository<OneTimePassword>().GetTableAsTracking()
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(x =>
                    x.Email == request.Email &&
                    x.Code == request.Code &&
                    x.Purpose == OtpPurpose.EmailVerification,
                    cancellationToken);

            if (otp == null)
                return _responseHandler.BadRequest<string>(_localizer[SharedResourcesKeys.Validation.Invalid]);

            if (otp.IsUsed)
                return _responseHandler.BadRequest<string>(_localizer[SharedResourcesKeys.Validation.OtpUsed]);

            if (otp.ExpiresAt < DateTime.UtcNow)
                return _responseHandler.BadRequest<string>(_localizer[SharedResourcesKeys.Validation.OtpExpired]);

            otp.IsUsed = true;
            await _unitOfWork.SaveChangesAsync(cancellationToken);


            return _responseHandler.Success<string>(null, _localizer[SharedResourcesKeys.Success.OtpValid]);
        }
    }
}
