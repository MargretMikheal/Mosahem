using mosahem.Domain.Entities.Opportunities;

namespace mosahem.Application.Interfaces.Repositories
{
    public interface IOpportunityApplicationRepository : IGenericRepository<OpportunityApplication>
    {
        Task<int> GetAcceptedApplicantsCount(Guid opportunityId, CancellationToken cancellationToken);
    }
}