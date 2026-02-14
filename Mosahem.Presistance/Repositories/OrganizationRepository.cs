using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Profiles;
using Mosahem.Application.Interfaces.Repositories.Specifications;

namespace mosahem.Persistence.Repositories
{
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(MosahmDbContext dbContext) : base(dbContext) { }
        public async Task<Organization?> GetOrganizationWithDetailsAsync(Guid organizationId, CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Organization>(o => o.Id == organizationId)
                .NoTracking()
                .AsSplitQuery()
                .Include(o => o.User)
                .Include(o => o.OrganizationFields)
                .Include("OrganizationFields.Field")
                .Include(o => o.Address)
                .Include("Address.City")
                .Include("Address.City.Governorate");

            return await FindFirstAsync(spec, cancellationToken);
        }
        public async Task<IReadOnlyList<Organization>> GetAllForListingAsync(CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Organization>()
                .NoTracking()
                .Include(o => o.User)
                .OrderByAsc(o => o.CreatedAt);

            return (await FindAllAsync(spec, cancellationToken)).ToList();
        }
    }
}