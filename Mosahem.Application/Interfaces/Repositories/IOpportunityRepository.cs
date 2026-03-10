using mosahem.Domain.Entities.Opportunities;

namespace mosahem.Application.Interfaces.Repositories
{
    // Opportunities
    public interface IOpportunityRepository : IGenericRepository<Opportunity>
    {
        Task<Opportunity?> GetOpportunityWithDetailsAsync(Guid opportunityId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Opportunity>> GetPendingOpportunitiesAsync(CancellationToken cancellationToken = default);
        Task<bool> IsOwnedByOrganizationAsync(Guid opportunityId, Guid organizationId, CancellationToken cancellationToken = default);
        Task<string?> GetOpportunityPhotoKeyAsync(Guid opportunityId, CancellationToken cancellationToken);
        Task<IReadOnlyList<Opportunity>> GetAcceptedOpportunitiesAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Opportunity>> GetRejectedOpportunitiesAsync(CancellationToken cancellationToken = default);
    }
}