using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Location;

namespace Mosahem.Application.Interfaces.Repositories
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<IReadOnlyList<Address>> GetOrganizationAddressesAsync(Guid organizationId, CancellationToken cancellationToken = default);
    }
}
