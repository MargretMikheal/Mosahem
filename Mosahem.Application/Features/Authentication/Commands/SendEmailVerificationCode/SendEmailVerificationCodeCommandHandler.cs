using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;
using Mosahem.Domain.Entities.Identity;
using Mosahem.Domain.Enums;

namespace mosahem.Application.Features.Authentication.Commands.SendEmailVerificationCode
{
    public class SendEmailVerificationCodeCommandHandler : IRequestHandler<SendEmailVerificationCodeCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public SendEmailVerificationCodeCommandHandler(
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IEmailTemplateService emailTemplateService,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(SendEmailVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var otpCode = new Random().Next(100000, 999999).ToString();

            var otpEntity = new OneTimePassword
            {
                Code = otpCode,
                Email = request.Email,
                UserId = null,
                Purpose = OtpPurpose.EmailVerification,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<OneTimePassword>().AddAsync(otpEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);


            string emailBody = _emailTemplateService.GenerateEmailVerificationEmail("New User", otpCode);

            await _emailService.SendEmailAsync(request.Email, "Verify Your Email - Mosahem", emailBody);

            return _responseHandler.Success<string>(null, _localizer[SharedResourcesKeys.Success.OtpSent]);
        }
    }
}