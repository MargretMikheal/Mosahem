using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Features.Authentication.Commands.ResetPassword;
using Mosahem.Application.Interfaces.Security;
using Mosahem.Domain.Entities.Identity;
using Mosahem.Domain.Enums;

namespace mosahem.Application.Features.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ResetPasswordCommandHandler(
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var generalError = _localizer[SharedResourcesKeys.General.OperationFailed].Value;

            var otp = await _unitOfWork.Repository<OneTimePassword>().GetTableAsTracking()
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(x =>
                    x.Email == request.Email &&
                    x.Code == request.Code &&
                    x.Purpose == OtpPurpose.PasswordReset, cancellationToken);

            if (otp == null || otp.IsUsed || otp.ExpiresAt < DateTime.UtcNow)
            {
                return _responseHandler.BadRequest<string>(
                    generalError,
                    new Dictionary<string, List<string>> { { "Otp", new List<string> { _localizer[SharedResourcesKeys.Validation.Invalid] } } });
            }

            var user = await _unitOfWork.Users.GetTableAsTracking()
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null)
            {
                return _responseHandler.BadRequest<string>(
                    generalError,
                    new Dictionary<string, List<string>> { { "Email", new List<string> { _localizer[SharedResourcesKeys.User.NotFound] } } });
            }

            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                user.PasswordHash = _passwordHasher.HashPassword(request.NewPassword);
                await _unitOfWork.Users.UpdateAsync(user);

                otp.IsUsed = true;
                await _unitOfWork.Repository<OneTimePassword>().UpdateAsync(otp);

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