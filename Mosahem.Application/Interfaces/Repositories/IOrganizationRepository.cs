using mosahem.Domain.Entities.Profiles;

namespace mosahem.Application.Interfaces.Repositories
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        Task<Organization?> GetOrganizationWithDetailsAsync(Guid organizationId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Organization>> GetAllForListingAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Guid organizationId, CancellationToken cancellationToken = default);
        Task<bool> IsVerifiedAsync(Guid organizationId, CancellationToken cancellationToken = default);
    }
}