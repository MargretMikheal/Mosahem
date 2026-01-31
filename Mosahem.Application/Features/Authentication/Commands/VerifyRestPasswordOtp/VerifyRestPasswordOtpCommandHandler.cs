using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Features.Authentication.Commands.VerifyRestPasswordOtp;
using Mosahem.Domain.Entities.Identity;
using Mosahem.Domain.Enums;

namespace mosahem.Application.Features.Authentication.Commands.VerifyRestPasswordOtp
{
    public class VerifyRestPasswordOtpCommandHandler : IRequestHandler<VerifyRestPasswordOtpCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public VerifyRestPasswordOtpCommandHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler, IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(VerifyRestPasswordOtpCommand request, CancellationToken cancellationToken)
        {
            var otp = await _unitOfWork.Repository<OneTimePassword>().GetTableNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(x =>
                    x.Email == request.Email &&
                    x.Code == request.Code &&
                    x.Purpose == OtpPurpose.PasswordReset, cancellationToken);

            var generalError = _localizer[SharedResourcesKeys.General.OperationFailed].Value;

            Dictionary<string, List<string>> CreateErrorDict(string message)
            {
                return new Dictionary<string, List<string>> { { "Otp", new List<string> { message } } };
            }

            if (otp == null)
                return _responseHandler.BadRequest<string>(generalError, CreateErrorDict(_localizer[SharedResourcesKeys.Validation.Invalid]));

            if (otp.IsUsed)
                return _responseHandler.BadRequest<string>(generalError, CreateErrorDict(_localizer[SharedResourcesKeys.Validation.OtpUsed]));

            if (otp.ExpiresAt < DateTime.UtcNow)
                return _responseHandler.BadRequest<string>(generalError, CreateErrorDict(_localizer[SharedResourcesKeys.Validation.OtpExpired]));

            return _responseHandler.Success<string>(null, _localizer[SharedResourcesKeys.Success.OtpValid]);
        }
    }
}