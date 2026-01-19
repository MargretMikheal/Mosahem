using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Opportunities;

namespace mosahem.Persistence.Repositories
{
    public class OpportunityRepository : GenericRepository<Opportunity>, IOpportunityRepository
    {
        public OpportunityRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}