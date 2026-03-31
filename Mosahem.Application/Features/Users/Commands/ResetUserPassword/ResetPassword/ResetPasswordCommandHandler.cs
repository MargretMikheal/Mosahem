using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;
using Mosahem.Application.Interfaces.Security;
using Mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Users.Commands.ResetUserPassword.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IOtpService _otpService;
        private readonly IPasswordHasher _passwordHasher;

        public ResetPasswordCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IOtpService otpService,
            IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _otpService = otpService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var generalError = _localizer[SharedResourcesKeys.General.OperationFailed].Value;
            var user = await _unitOfWork.Users.GetTableAsTracking()
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null)
            {
                return _responseHandler.BadRequest<string>(
                    generalError,
                    new Dictionary<string, List<string>> { { "Email", new List<string> { _localizer[SharedResourcesKeys.User.NotFound] } } });
            }

            #region OTP check
            try
            {
                await _otpService.MakeAsUsedAsync(
                    user.Id,
                    request.Email,
                    request.Code,
                    OtpPurpose.PasswordReset,
                    cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                return _responseHandler.BadRequest<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        {"Otp" , new() { _localizer[ex.Message] } }
                    });
            }
            #endregion

            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                user.PasswordHash = _passwordHasher.HashPassword(request.NewPassword);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return _responseHandler.Success<string>(null, _localizer[SharedResourcesKeys.Success.PasswordReset]);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _responseHandler.BadRequest<string>(
                    generalError,
                    new Dictionary<string, List<string>> { { "Exception", new List<string> { ex.Message } } });
            }
        }
    }
}