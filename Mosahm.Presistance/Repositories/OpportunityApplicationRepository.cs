using Mosahm.Application.Interfaces.Repositories;
using Mosahm.Domain.Entities.Opportunities;

namespace Mosahm.Persistence.Repositories
{
    public class OpportunityApplicationRepository : GenericRepository<OpportunityApplication>, IOpportunityApplicationRepository
    {
        public OpportunityApplicationRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}