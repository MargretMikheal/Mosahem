using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;
using Mosahem.Domain.Enums;

namespace mosahem.Application.Features.Authentication.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IOtpService _otpService;

        public VerifyEmailCommandHandler(
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

        public async Task<Response<string>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _otpService.VerifyOtpAsync(
                   null,
                   request.Email,
                   request.Code,
                   OtpPurpose.EmailVerification,
                   cancellationToken);

                await _otpService.MakeAsUsedAsync(
                    null,
                    request.Email,
                    request.Code,
                    OtpPurpose.EmailVerification,
                    cancellationToken);

                return _responseHandler.Success<string>(null!, _localizer[SharedResourcesKeys.Success.OtpValid]);
            }
            catch (InvalidOperationException ex)
            {
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>>
                    {
                        { "Otp", new List<string> { _localizer[ex.Message] } }
                    });
            }
        }
    }
}