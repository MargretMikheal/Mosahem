using Mosahm.Application.Interfaces.Repositories;
using Mosahm.Domain.Entities.Location;

namespace Mosahm.Persistence.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}