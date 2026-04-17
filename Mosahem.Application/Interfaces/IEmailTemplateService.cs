namespace Mosahem.Application.Interfaces
{
    public interface IEmailTemplateService
    {
        string GeneratePasswordResetEmail(string userName, string otpCode);
        string GenerateEmailVerificationEmail(string userName, string otpCode);
        string GenerateChangeEmailVerificationEmail(string userName, string otpCode);
        string GenerateVolunteerAcceptedEmail(string volunteerName, string opportunityName, string opportunityUrl, string? imageUrl);
        string GenerateVolunteerRejectionEmail(string volunteerName, string opportunityName, string opportunitiesUrl, string? imageUrl);

    }
}
