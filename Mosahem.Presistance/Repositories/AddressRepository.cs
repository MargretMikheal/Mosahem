using mosahem.Domain.Entities.Location;
using mosahem.Persistence;
using mosahem.Persistence.Repositories;
using Mosahem.Application.Interfaces.Repositories;
using Mosahem.Application.Interfaces.Repositories.Specifications;

namespace Mosahem.Persistence.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(MosahmDbContext dbContext) : base(dbContext) { }

        public async Task<IReadOnlyList<Address>> GetOrganizationAddressesAsync(Guid organizationId, CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Address>(address => address.OrganizationId == organizationId)
                .NoTracking()
                .Include(address => address.City)
                .Include("City.Governorate");

            return (await FindAllAsync(spec, cancellationToken)).ToList();
        }
    }
}
