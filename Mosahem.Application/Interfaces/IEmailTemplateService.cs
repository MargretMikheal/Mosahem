namespace Mosahem.Application.Interfaces
{
    public interface IEmailTemplateService
    {
        string GenerateOtpEmail(string userName, string otpCode);
    }
}
