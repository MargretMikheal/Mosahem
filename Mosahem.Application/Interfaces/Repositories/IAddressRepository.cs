using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Location;

namespace Mosahem.Application.Interfaces.Repositories
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<IReadOnlyList<Address>> GetOrganizationAddressesAsync(Guid organizationId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Address>> GetOpportunityAddressAsync(Guid OpportunityId, CancellationToken cancellationToken);
        Task<Address?> GetByIdAndOrganizationId(Guid addressId, Guid organizationId, CancellationToken cancellationToken);
    }
}
