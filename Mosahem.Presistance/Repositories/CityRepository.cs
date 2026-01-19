using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Location;

namespace mosahem.Persistence.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}