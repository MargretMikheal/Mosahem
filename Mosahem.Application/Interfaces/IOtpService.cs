using Mosahem.Domain.Entities.Identity;
using Mosahem.Domain.Enums;

namespace Mosahem.Application.Interfaces
{
    public interface IOtpService
    {
        Task<OneTimePassword> GenerateOtpAsync(Guid? userId, string email, OtpPurpose purpose, CancellationToken cancellationToken);
        Task SendOtpAsync(OneTimePassword otp, string userName, CancellationToken cancellationToken);
        Task VerifyOtpAsync(Guid? userId, string email, string code, OtpPurpose purpose, CancellationToken cancellationToken);
        Task MakeAsUsedAsync(Guid? userId, string email, string code, OtpPurpose purpose, CancellationToken cancellationToken);
    }
}
