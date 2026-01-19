using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Profiles;

namespace mosahem.Persistence.Repositories
{
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}