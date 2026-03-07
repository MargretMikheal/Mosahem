using mosahem.Domain.Entities.Opportunities;

namespace mosahem.Application.Interfaces.Repositories
{
    // Opportunities
    public interface IOpportunityRepository : IGenericRepository<Opportunity>
    {
        Task<Opportunity?> GetOpportunityWithDetailsAsync(Guid opportunityId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Opportunity>> GetPendingOpportunitiesAsync(CancellationToken cancellationToken = default);
    }
}