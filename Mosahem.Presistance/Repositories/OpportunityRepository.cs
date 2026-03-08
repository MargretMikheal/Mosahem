using Microsoft.EntityFrameworkCore;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Opportunities;

namespace mosahem.Persistence.Repositories
{
    public class OpportunityRepository : GenericRepository<Opportunity>, IOpportunityRepository
    {
        public OpportunityRepository(MosahmDbContext dbContext) : base(dbContext) { }

        public async Task<string?> GetOpportunityPhotoKeyAsync(Guid opportunityId, CancellationToken cancellationToken)
        {
            return await GetTableNoTracking()
                .Where(o => o.Id == opportunityId).
                Select(o => o.PhotoKey)
                .FirstOrDefaultAsync();
        }
    }
}