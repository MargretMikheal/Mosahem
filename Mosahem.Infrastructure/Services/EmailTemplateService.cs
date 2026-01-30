using Mosahem.Application.Interfaces;

namespace mosahem.Infrastructure.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public string GeneratePasswordResetEmail(string userName, string otpCode)
        {
            string content = $@"
                <p style=""color: #666666; font-size: 16px;"">Hello <strong>{userName}</strong>,</p>
                <p style=""color: #666666; font-size: 16px;"">We received a request to reset your password. Use the code below to proceed:</p>
                
                <div style=""text-align: center; margin: 30px 0;"">
                    <span style=""background-color: #f4f4f4; border: 2px dashed #4A90E2; color: #4A90E2; padding: 15px 30px; font-size: 24px; font-weight: bold; letter-spacing: 5px; border-radius: 5px;"">
                        {otpCode}
                    </span>
                </div>

                <p style=""color: #666666; font-size: 14px;"">This code is valid for 10 minutes. If you did not request a password reset, please ignore this email.</p>
            ";

            return GenerateGeneralLayout("Password Reset Request", content);
        }

        public string GenerateEmailVerificationEmail(string userName, string otpCode)
        {
            string content = $@"
                <p style=""color: #666666; font-size: 16px;"">Welcome to Mosahem, <strong>{userName}</strong>!</p>
                <p style=""color: #666666; font-size: 16px;"">Thank you for joining us. To activate your account and verify your email address, please use the code below:</p>
                
                <div style=""text-align: center; margin: 30px 0;"">
                    <span style=""background-color: #f4f4f4; border: 2px dashed #28a745; color: #28a745; padding: 15px 30px; font-size: 24px; font-weight: bold; letter-spacing: 5px; border-radius: 5px;"">
                        {otpCode}
                    </span>
                </div>

                <p style=""color: #666666; font-size: 14px;"">This code is valid for 10 minutes. If you did not sign up for Mosahem, please ignore this email.</p>
            ";

            return GenerateGeneralLayout("Verify Your Email", content);
        }

        private string GenerateGeneralLayout(string title, string content)
        {
            return $@"
                <div style=""font-family: Arial, sans-serif; max-width: 600px; margin: auto; border: 1px solid #e0e0e0; border-radius: 10px; overflow: hidden;"">
                    <div style=""background-color: #4A90E2; padding: 20px; text-align: center; color: white;"">
                        <h1 style=""margin: 0;"">Mosahem</h1>
                    </div>
                    <div style=""padding: 30px; background-color: #ffffff;"">
                        <h2 style=""color: #333333;"">{title}</h2>
                        {content}
                    </div>
                    <div style=""background-color: #f9f9f9; padding: 15px; text-align: center; color: #999999; font-size: 12px;"">
                        &copy; {DateTime.Now.Year} Mosahem Platform. All rights reserved.
                    </div>
                </div>";
        }
    }
}