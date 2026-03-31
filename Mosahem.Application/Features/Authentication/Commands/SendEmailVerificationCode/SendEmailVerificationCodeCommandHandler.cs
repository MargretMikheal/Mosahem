using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;
using Mosahem.Domain.Enums;

namespace mosahem.Application.Features.Authentication.Commands.SendEmailVerificationCode
{
    public class SendEmailVerificationCodeCommandHandler : IRequestHandler<SendEmailVerificationCodeCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IOtpService _otpService;

        public SendEmailVerificationCodeCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IOtpService otpService)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _otpService = otpService;
        }

        public async Task<Response<string>> Handle(SendEmailVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var otp = await _otpService.GenerateOtpAsync(null!, request.Email, OtpPurpose.EmailVerification, cancellationToken);
                await _otpService.SendOtpAsync(otp, "User", cancellationToken);

                return _responseHandler.Success<string>(null!, _localizer[SharedResourcesKeys.Success.OtpSent]);
            }
            catch (InvalidOperationException ex)
            {
                return _responseHandler.BadRequest<string>(_localizer[ex.Message]);
            }
        }
    }
}