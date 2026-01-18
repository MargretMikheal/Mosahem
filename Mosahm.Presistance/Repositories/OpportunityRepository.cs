using Mosahm.Application.Interfaces.Repositories;
using Mosahm.Domain.Entities.Opportunities;

namespace Mosahm.Persistence.Repositories
{
    public class OpportunityRepository : GenericRepository<Opportunity>, IOpportunityRepository
    {
        public OpportunityRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}