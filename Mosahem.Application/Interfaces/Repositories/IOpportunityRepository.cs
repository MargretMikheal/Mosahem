using mosahem.Domain.Entities.Opportunities;

namespace mosahem.Application.Interfaces.Repositories
{
    // Opportunities
    public interface IOpportunityRepository : IGenericRepository<Opportunity>
    {
        Task<string?> GetOpportunityPhotoKeyAsync(Guid opportunityId, CancellationToken cancellationToken);
    }
}