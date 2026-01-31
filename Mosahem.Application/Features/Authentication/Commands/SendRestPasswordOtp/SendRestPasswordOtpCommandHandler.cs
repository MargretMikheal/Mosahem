using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;
using Mosahem.Domain.Entities.Identity;
using Mosahem.Domain.Enums;

namespace mosahem.Application.Features.Authentication.Commands.SendRestPasswordOtp
{
    public class SendRestPasswordOtpCommandHandler : IRequestHandler<SendRestPasswordOtpCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IEmailTemplateService _emailTemplateService;

        public SendRestPasswordOtpCommandHandler(
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IEmailTemplateService emailTemplateService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _emailTemplateService = emailTemplateService;
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
                var otpCode = new Random().Next(100000, 999999).ToString();

                var otpEntity = new OneTimePassword
                {
                    Code = otpCode,
                    Email = request.Email,
                    UserId = user.Id,
                    Purpose = OtpPurpose.PasswordReset,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                    IsUsed = false,
                };

                await _unitOfWork.Repository<OneTimePassword>().AddAsync(otpEntity, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                string emailBody = _emailTemplateService.GeneratePasswordResetEmail(user.FullName, otpCode);

                await _emailService.SendEmailAsync(user.Email, "Reset Your Password - Mosahem", emailBody);

                return _responseHandler.Success<string>(null, _localizer[SharedResourcesKeys.Success.OtpSent]);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<string>(
                     _localizer[SharedResourcesKeys.General.OperationFailed],
                     new Dictionary<string, List<string>> { { "EmailService", new List<string> { ex.Message } } });
            }
        }
    }
}