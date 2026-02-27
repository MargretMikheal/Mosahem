using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Persistence;
using mosahem.Persistence.Repositories;
using mosahem.Presistence.BackgroundServices;
using Mosahem.Application.Settings;

namespace mosahem.Presistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<MosahmDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("MainConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.Configure<UserCleanupSettings>(configuration.GetSection("UserCleanupSettings"));
            services.Configure<TempFilesCleanupSettings>(configuration.GetSection("TempFilesCleanupSettings"));
            services.AddHostedService<DeletedUsersCleanupService>();
            services.AddHostedService<TemporaryFilesCleanupService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();

            services.AddScoped<IOpportunityRepository, OpportunityRepository>();
            services.AddScoped<IOpportunityApplicationRepository, OpportunityApplicationRepository>();
            services.AddScoped<IOpportunityCommentRepository, OpportunityCommentRepository>();

            services.AddScoped<IFieldRepository, FieldRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();

            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IGovernorateRepository, GovernorateRepository>();


            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionAnswerRepository, QuestionAnswerRepository>();

            return services;
        }
    }
}