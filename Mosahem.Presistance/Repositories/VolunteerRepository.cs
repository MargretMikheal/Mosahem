using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Profiles;

namespace mosahem.Persistence.Repositories
{
    public class VolunteerRepository : GenericRepository<Volunteer>, IVolunteerRepository
    {
        public VolunteerRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}