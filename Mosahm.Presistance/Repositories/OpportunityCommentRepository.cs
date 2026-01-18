using Mosahm.Application.Interfaces.Repositories;
using Mosahm.Domain.Entities.Opportunities;

namespace Mosahm.Persistence.Repositories
{
    public class OpportunityCommentRepository : GenericRepository<OpportunityComment>, IOpportunityCommentRepository
    {
        public OpportunityCommentRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}