using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Opportunities;

namespace mosahem.Persistence.Repositories
{
    public class OpportunityApplicationRepository : GenericRepository<OpportunityApplication>, IOpportunityApplicationRepository
    {
        public OpportunityApplicationRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}