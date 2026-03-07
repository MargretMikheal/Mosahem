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
    }
}
