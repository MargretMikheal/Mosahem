using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using mosahem.Application.Settings;
using mosahem.Application.Interfaces; 
using mosahem.Infrastructure.Services; 
using System.Reflection;
using System.Text;

namespace mosahem.Infrastructure
{
    public static class AuthenticationServiceRegistration
    {
        public static IServiceCollection AddAuthenticationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // JWT settings
            services.Configure<JwtSettings>(
                configuration.GetSection("JwtSettings"));

            // Auth services
            services.AddScoped<IAuthService, AuthService>();

            // API behavior
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // JWT authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    configuration["JwtSettings:SecretKey"]))
                    };
                });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
