using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;
using Mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Users.Commands.ResetUserPassword.SendRestPasswordOtp
{
    public class SendRestPasswordOtpCommandHandler : IRequestHandler<SendRestPasswordOtpCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IOtpService _otpService;

        public SendRestPasswordOtpCommandHandler(
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

        public async Task<Response<string>> Handle(SendRestPasswordOtpCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetTableNoTracking()
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null)
            {
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>> { { "Email", new List<string> { _localizer[SharedResourcesKeys.User.NotFound] } } });
            }

            try
            {
                var otp = await _otpService.GenerateOtpAsync(user.Id, request.Email, OtpPurpose.PasswordReset, cancellationToken);

                await _otpService.SendOtpAsync(otp, user.FullName, cancellationToken);


                return _responseHandler.Success<string>(null!, _localizer[SharedResourcesKeys.Success.OtpSent]);
            }
            catch (InvalidOperationException ex)
            {
                return _responseHandler.BadRequest<string>(_localizer[ex.Message]);
            }
        }
    }
}