namespace Mosahem.Application.Features.Users.Commands.ChangeEmail.ChangeEmailOtpVerification
{
    public class ChangeEmailOtpVerificationRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
