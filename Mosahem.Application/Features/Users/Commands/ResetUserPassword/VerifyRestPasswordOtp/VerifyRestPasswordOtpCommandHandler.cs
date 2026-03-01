using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;
using Mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Users.Commands.ResetUserPassword.VerifyRestPasswordOtp
{
    public class VerifyRestPasswordOtpCommandHandler : IRequestHandler<VerifyRestPasswordOtpCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IOtpService _otpService;

        public VerifyRestPasswordOtpCommandHandler(
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

        public async Task<Response<string>> Handle(VerifyRestPasswordOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetTableNoTracking()
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
            if (user == null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);

            try
            {
                await _otpService.VerifyOtpAsync(
                   user.Id,
                   request.Email,
                   request.Code,
                   OtpPurpose.PasswordReset,
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