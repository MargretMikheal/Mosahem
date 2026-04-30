using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Entities.Profiles;
using mosahem.Domain.Enums;

namespace mosahem.Application.Interfaces.Repositories
{
    public interface IOpportunityApplicationRepository : IGenericRepository<OpportunityApplication>
    {
        Task<int> GetAcceptedApplicantsCountAsync(Guid opportunityId, CancellationToken cancellationToken);
        Task<bool> IsExistAsync(Guid volunteerId, Guid opportunityId, CancellationToken cancellationToken);
        Task<(IReadOnlyList<Volunteer>, int totalCount)> GetApplicantsByStatusPageAsync(Guid opportunityId, ApplicantStatus status, int page, int pageSize, CancellationToken cancellationToken);
        Task<(IReadOnlyList<Volunteer>, int totalCount)> GetOrganizationVolunteersByStatusPageAsync(Guid organizationId, ApplicantStatus status, int page, int pageSize, CancellationToken cancellationToken);
        Task<OpportunityApplication?> GetByVolunteerAndOpportunityIdAsync(Guid volunteerId, Guid opportunityId, CancellationToken cancellationToken);
    }
}