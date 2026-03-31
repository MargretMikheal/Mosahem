namespace Mosahem.Infrastructure.Services
{
    using Microsoft.EntityFrameworkCore;
    using mosahem.Application.Interfaces;
    using mosahem.Application.Interfaces.Repositories;
    using mosahem.Application.Resources;
    using Mosahem.Application.Interfaces;
    using Mosahem.Application.Interfaces.Repositories.Specifications;
    using Mosahem.Domain.Entities.Identity;
    using Mosahem.Domain.Enums;
    using System.Security.Cryptography;
    using System.Threading;

    public class OtpService : IOtpService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _templateService;

        public OtpService(
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IEmailTemplateService templateService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _templateService = templateService;
        }

        public async Task<OneTimePassword> GenerateOtpAsync(
            Guid? userId,
            string email,
            OtpPurpose purpose,
            CancellationToken cancellationToken = default)
        {
            var spec = new Specification<OneTimePassword>(otp =>
            otp.UserId == userId &&
            otp.Email == email &&
            otp.Purpose == purpose &&
            otp.CreatedAt > DateTime.UtcNow.AddMinutes(-1));

            var oldOtp = await _unitOfWork.Repository<OneTimePassword>()
                .FindFirstAsync(spec, cancellationToken);

            if (oldOtp is not null)
                throw new InvalidOperationException(SharedResourcesKeys.Validation.OtpAlreadySent);

            await _unitOfWork.Repository<OneTimePassword>()
                .GetTableAsTracking()
                .Where(otp =>
                otp.UserId == userId &&
                otp.Email == email &&
                otp.Purpose == purpose)
                .ExecuteUpdateAsync(o => o.SetProperty(otp => otp.IsUsed, true), cancellationToken);

            var otpCode = RandomNumberGenerator
                .GetInt32(100000, 999999)
                .ToString();

            var otpEntity = new OneTimePassword
            {
                Email = email,
                Code = otpCode,
                UserId = userId,
                Purpose = purpose,
                IsUsed = false,
                IsVerified = false,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10)
            };

            await _unitOfWork.Repository<OneTimePassword>()
                .AddAsync(otpEntity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return otpEntity;
        }

        public async Task SendOtpAsync(OneTimePassword otp, string userName, CancellationToken cancellationToken = default)
        {
            string subject;
            string body;

            switch (otp.Purpose)
            {
                case OtpPurpose.EmailVerification:
                    subject = "Verify Your Email - Mosahem";
                    body = _templateService.GenerateEmailVerificationEmail(
                        userName,
                        otp.Code);
                    break;
                case OtpPurpose.ChangeEmailVerification:
                    subject = "Confirm Email Change - Mosahem";
                    body = _templateService.GenerateChangeEmailVerificationEmail(
                        userName,
                        otp.Code);
                    break;
                case OtpPurpose.PasswordReset:
                    subject = "Reset Your Password - Mosahem";
                    body = _templateService.GeneratePasswordResetEmail(
                        userName,
                        otp.Code);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(otp.Purpose));
            }

            await _emailService.SendEmailAsync(
                otp.Email,
                subject,
                body,
                cancellationToken);
        }
        public async Task VerifyOtpAsync(
            Guid? userId,
            string email,
            string code,
            OtpPurpose purpose,
            CancellationToken cancellationToken = default)
        {
            var spec = new Specification<OneTimePassword>(otp =>
            otp.UserId == userId &&
            otp.Email == email &&
            otp.Purpose == purpose &&
            otp.Code == code);

            var otp = await _unitOfWork.Repository<OneTimePassword>()
                .FindFirstAsync(spec, cancellationToken);

            if (otp is null || otp.IsUsed || otp.IsVerified)
                throw new InvalidOperationException(SharedResourcesKeys.Validation.Invalid);

            if (otp.ExpiresAt < DateTime.UtcNow)
                throw new InvalidOperationException(SharedResourcesKeys.Validation.OtpExpired);

            otp.IsVerified = true;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task MakeAsUsedAsync(
            Guid? userId,
            string email,
            string code,
            OtpPurpose purpose,
            CancellationToken cancellationToken = default)
        {
            var spec = new Specification<OneTimePassword>(otp =>
            otp.UserId == userId &&
            otp.Email == email &&
            otp.Purpose == purpose &&
            otp.Code == code);

            var otp = await _unitOfWork.Repository<OneTimePassword>()
                .FindFirstAsync(spec, cancellationToken);

            if (otp is null || !otp.IsVerified || otp.IsUsed || otp.ExpiresAt < DateTime.UtcNow)
                throw new InvalidOperationException(SharedResourcesKeys.Validation.Invalid);

            otp.IsUsed = true;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
