using Mosahm.Application.Interfaces.Repositories;
using Mosahm.Domain.Entities.Profiles;

namespace Mosahm.Persistence.Repositories
{
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}