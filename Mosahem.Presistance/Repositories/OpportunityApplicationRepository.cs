using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Enums;
using Mosahem.Application.Interfaces.Repositories.Specifications;

namespace mosahem.Persistence.Repositories
{
    public class OpportunityApplicationRepository : GenericRepository<OpportunityApplication>, IOpportunityApplicationRepository
    {
        public OpportunityApplicationRepository(MosahmDbContext dbContext) : base(dbContext) { }

        public async Task<int> GetAcceptedApplicantsCount(Guid opportunityId, CancellationToken cancellationToken)
        {
            var spec = new Specification<OpportunityApplication>(oa => oa.OpportunityId == opportunityId && oa.ApplicantStatus == ApplicantStatus.Accepted)
                .NoTracking();

            return await CountAsync(spec, cancellationToken);
        }
    }
}