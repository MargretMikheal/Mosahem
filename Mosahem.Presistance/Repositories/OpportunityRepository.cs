using Microsoft.EntityFrameworkCore;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Enums;
using Mosahem.Application.Interfaces.Repositories.Specifications;

namespace mosahem.Persistence.Repositories
{
    public class OpportunityRepository : GenericRepository<Opportunity>, IOpportunityRepository
    {
        public OpportunityRepository(MosahmDbContext dbContext) : base(dbContext)
        { }
        public async Task<Opportunity?> GetOpportunityWithDetailsAsync(Guid opportunityId, CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Opportunity>(opportunity => opportunity.Id == opportunityId)
                .NoTracking()
                .AsSplitQuery()
                .Include("Organization.User")
                .Include("Address.City.Governorate")
                .Include("OpportunitySkills.Skill")
                .Include("OpportunityFields.Field")
                .Include("OpportunityLikes")
                .Include("OpportunityComments")
                .Include("OpportunitySaves")
                .Include("Questions");

            return await FindFirstAsync(spec, cancellationToken);
        }

        public async Task<IReadOnlyList<Opportunity>> GetPendingOpportunitiesAsync(CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Opportunity>(opportunity => opportunity.VerificationStatus == VerficationStatus.Pending)
                .NoTracking()
                .Include("Organization.User")
                .OrderByAsc(opportunity => opportunity.CreatedAt);

            return (await FindAllAsync(spec, cancellationToken)).ToList();
        }
        public async Task<bool> IsOwnedByOrganizationAsync(Guid opportunityId, Guid organizationId, CancellationToken cancellationToken = default)
        {
            var specification = new Specification<Opportunity>(
                opportunity => opportunity.Id == opportunityId && opportunity.OrganizationId == organizationId);

            return await CountAsync(specification, cancellationToken) > 0;
        }

        public async Task<string?> GetOpportunityPhotoKeyAsync(Guid opportunityId, CancellationToken cancellationToken)
        {
            return await GetTableNoTracking()
                .Where(o => o.Id == opportunityId).
                Select(o => o.PhotoKey)
                .FirstOrDefaultAsync();
        }
    }
}
