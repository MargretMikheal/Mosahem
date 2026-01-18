using FluentEmail.Smtp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mosahm.Application.Interfaces;
using Mosahm.Application.Settings;
using Mosahm.Infrastructure.Services;
using Mosahm.Persistence;
using System.Net;
using System.Net.Mail;



namespace Mosahm.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            

            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

            var mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();

            if (mailSettings == null)
                throw new InvalidOperationException("MailSettings section is missing in appsettings.json");

            var smtpClient = new SmtpClient(mailSettings.Host)
            {
                Port = mailSettings.Port,
                Credentials = new NetworkCredential(mailSettings.Email, mailSettings.Password),
                EnableSsl = mailSettings.EnableSsl
            };

            services
                .AddFluentEmail(mailSettings.Email, mailSettings.DisplayName)
                .AddRazorRenderer()
                .AddSmtpSender(smtpClient);

            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
