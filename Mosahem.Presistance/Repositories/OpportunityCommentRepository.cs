using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Opportunities;

namespace mosahem.Persistence.Repositories
{
    public class OpportunityCommentRepository : GenericRepository<OpportunityComment>, IOpportunityCommentRepository
    {
        public OpportunityCommentRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}