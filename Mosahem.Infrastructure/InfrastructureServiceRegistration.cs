using Amazon.S3;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mosahem.Application.Interfaces;
using mosahem.Application.Settings;
using mosahem.Infrastructure.Services;
using Mosahem.Application.Interfaces;
using Mosahem.Application.Interfaces.Security;
using Mosahem.Application.Settings;
using Mosahem.Infrastructure.Services;
using Mosahem.Infrastructure.Services.Security;
using System.Net;
using System.Net.Mail;



namespace mosahem.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            #region MailSettings
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
            #endregion

            #region Storage Settings 

            services.Configure<FileStorageSettings>(configuration.GetSection("FileStorageSettings"));


            var fileSettings = configuration.GetSection("FileStorageSettings").Get<FileStorageSettings>();

            if (fileSettings != null)
            {
                var s3Config = new AmazonS3Config
                {
                    ServiceURL = fileSettings.ServiceUrl
                };

                var credentials = new Amazon.Runtime.BasicAWSCredentials(fileSettings.AccessKey, fileSettings.SecretKey);

                services.AddSingleton<IAmazonS3>(_ => new AmazonS3Client(credentials, s3Config));
            }

            services.AddScoped<IFileService, FileService>();
            #endregion
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            return services;
        }
    }
}
