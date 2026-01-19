using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Location;

namespace mosahem.Persistence.Repositories
{
    public class GovernorateRepository : GenericRepository<Governorate>, IGovernorateRepository
    {
        public GovernorateRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}