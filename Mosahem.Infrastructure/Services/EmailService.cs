using FluentEmail.Core;
using mosahem.Application.Interfaces;

namespace mosahem.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }

        public async Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
        {
            var response = await _fluentEmail
                .To(to)
                .Subject(subject)
                .Body(body, isHtml: true)
                .SendAsync(cancellationToken);

            if (!response.Successful)
            {
                var errorMsg = string.Join(", ", response.ErrorMessages);
                throw new InvalidOperationException($"Failed to send email. Errors: {errorMsg}");
            }
        }
    }
}