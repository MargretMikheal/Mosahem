using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using mosahem.Domain.Enums;
using mosahem.Persistence;
using Mosahem.Application.Settings;

namespace mosahem.Presistence.BackgroundServices
{
    public class DeletedUsersCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<DeletedUsersCleanupService> _logger;
        private readonly IOptionsMonitor<UserCleanupSettings> _settings;

        public DeletedUsersCleanupService(
            IServiceScopeFactory scopeFactory,
            ILogger<DeletedUsersCleanupService> logger,
            IOptionsMonitor<UserCleanupSettings> settings)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _settings = settings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var settings = _settings.CurrentValue;

                if (settings.Enabled)
                {
                    try
                    {
                        await CleanupDeletedUsersAsync(settings, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Deleted users cleanup job failed.");
                    }
                }

                var delay = TimeSpan.FromHours(Math.Max(1, settings.RunEveryHours));
                await Task.Delay(delay, stoppingToken);
            }
        }

        private async Task CleanupDeletedUsersAsync(UserCleanupSettings settings, CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MosahmDbContext>();

            var cutoff = DateTime.UtcNow.AddDays(-Math.Abs(settings.RetentionDays));
            var batchSize = Math.Max(1, settings.BatchSize);

            var usersToPurge = await dbContext.Users
                .IgnoreQueryFilters()
                .AsNoTracking()
                .Where(u => u.IsDeleted && u.DeletedAt.HasValue && u.DeletedAt.Value <= cutoff)
                .OrderBy(u => u.DeletedAt)
                .Take(batchSize)
                .Select(u => new { u.Id, u.Role })
                .ToListAsync(cancellationToken);

            if (usersToPurge.Count == 0)
                return;

            foreach (var user in usersToPurge)
            {
                await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    await dbContext.RefreshTokens
                        .Where(rt => rt.UserId == user.Id)
                        .ExecuteDeleteAsync(cancellationToken);

                    if (user.Role == UserRole.Volunteer)
                    {
                        await DeleteVolunteerGraphAsync(dbContext, user.Id, cancellationToken);
                    }
                    else if (user.Role == UserRole.Organization)
                    {
                        await DeleteOrganizationGraphAsync(dbContext, user.Id, cancellationToken);
                    }

                    await dbContext.Users
                        .IgnoreQueryFilters()
                        .Where(u => u.Id == user.Id)
                        .ExecuteDeleteAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogWarning(ex, "Failed to purge soft-deleted user {UserId}.", user.Id);
                }
            }
        }

        private static async Task DeleteVolunteerGraphAsync(MosahmDbContext dbContext, Guid volunteerId, CancellationToken cancellationToken)
        {
            await dbContext.QuestionAnswers
                .Where(qa => qa.VolunteerId == volunteerId)
                .ExecuteDeleteAsync(cancellationToken);

            await dbContext.OpportunityApplications
                .Where(a => a.VolunteerId == volunteerId)
                .ExecuteDeleteAsync(cancellationToken);

            await dbContext.OpportunityComments
                .Where(c => c.VolunteerId == volunteerId)
                .ExecuteDeleteAsync(cancellationToken);

            await dbContext.OpportunityLikes
                .Where(l => l.VolunteerId == volunteerId)
                .ExecuteDeleteAsync(cancellationToken);

            await dbContext.OpportunitySaves
                .Where(s => s.VolunteerId == volunteerId)
                .ExecuteDeleteAsync(cancellationToken);

            await dbContext.OrganizationFollowers
                .Where(f => f.VolunteerId == volunteerId)
                .ExecuteDeleteAsync(cancellationToken);

            await dbContext.VolunteerFields
                .Where(vf => vf.VolunteerId == volunteerId)
                .ExecuteDeleteAsync(cancellationToken);

            await dbContext.VolunteerSkills
                .Where(vs => vs.VolunteerId == volunteerId)
                .ExecuteDeleteAsync(cancellationToken);

            await dbContext.Addresses
                .Where(a => a.VolunteerId == volunteerId)
                .ExecuteDeleteAsync(cancellationToken);

            await dbContext.Volunteers
                .IgnoreQueryFilters()
                .Where(v => v.Id == volunteerId)
                .ExecuteDeleteAsync(cancellationToken);
        }

        private static async Task DeleteOrganizationGraphAsync(MosahmDbContext dbContext, Guid organizationId, CancellationToken cancellationToken)
        {
            await dbContext.OrganizationFollowers
                .Where(f => f.OrganizationId == organizationId)
                .ExecuteDeleteAsync(cancellationToken);

            await dbContext.OrganizationFields
                .Where(of => of.OrganizationId == organizationId)
                .ExecuteDeleteAsync(cancellationToken);

            var opportunityIds = await dbContext.Opportunities
                .Where(o => o.OrganizationId == organizationId)
                .Select(o => o.Id)
                .ToListAsync(cancellationToken);

            if (opportunityIds.Count > 0)
            {
                await dbContext.OpportunityApplications
                    .Where(a => opportunityIds.Contains(a.OpportunityId))
                    .ExecuteDeleteAsync(cancellationToken);

                await dbContext.OpportunityComments
                    .Where(c => opportunityIds.Contains(c.OpportunityId))
                    .ExecuteDeleteAsync(cancellationToken);

                await dbContext.OpportunityLikes
                    .Where(l => opportunityIds.Contains(l.OpportunityId))
                    .ExecuteDeleteAsync(cancellationToken);

                await dbContext.OpportunitySaves
                    .Where(s => opportunityIds.Contains(s.OpportunityId))
                    .ExecuteDeleteAsync(cancellationToken);

                await dbContext.OpportunitySkills
                    .Where(os => opportunityIds.Contains(os.OpportunityId))
                    .ExecuteDeleteAsync(cancellationToken);

                await dbContext.OpportunityFields
                    .Where(of => opportunityIds.Contains(of.OpportunityId))
                    .ExecuteDeleteAsync(cancellationToken);

                await dbContext.Opportunities
                    .Where(o => o.OrganizationId == organizationId)
                    .ExecuteDeleteAsync(cancellationToken);
            }

            await dbContext.Addresses
                .Where(a => a.OrganizationId == organizationId)
                .ExecuteDeleteAsync(cancellationToken);

            await dbContext.Organizations
                .IgnoreQueryFilters()
                .Where(o => o.Id == organizationId)
                .ExecuteDeleteAsync(cancellationToken);
        }
    }
}
