namespace Mosahem.Application.Interfaces
{
    public interface IEmailTemplateService
    {
        string GeneratePasswordResetEmail(string userName, string otpCode);
        string GenerateEmailVerificationEmail(string userName, string otpCode);
    }
}
