using Mosahm.Application.Interfaces.Repositories;
using Mosahm.Domain.Entities.Location;

namespace Mosahm.Persistence.Repositories
{
    public class GovernorateRepository : GenericRepository<Governorate>, IGovernorateRepository
    {
        public GovernorateRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}