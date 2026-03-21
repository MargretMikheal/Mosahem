using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Enums;

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
        Task<(IReadOnlyList<Opportunity>, int totalCount)> GetOrganizationOpportunitiesByVerificationStatusPageAsync(Guid organizationId, VerficationStatus verficationStatus, int page, int pageSize, CancellationToken cancellationToken);
        Task<(IReadOnlyList<Opportunity>, int totalCount)> GetOrganizationOpportunitiesByStatusPageAsync(Guid organizationId, OpportunityStatus status, int page, int pageSize, CancellationToken cancellationToken);
        Task<(IReadOnlyList<Opportunity>, int totalCount)> GetAllOpportunitiesPageAsync(
            string? search,
            Guid? governateId,
            OpportunityStatus? status,
            OpportunityWorkType? workType,
            OpportunityLocationType? locationType,
            DateTime? startDate,
            List<Guid>? fieldIds,
            List<Guid>? requiredSkillIds,
            List<Guid>? providedSkillIds,
            int page,
            int pageSize,
            CancellationToken cancellationToken);
    }
}