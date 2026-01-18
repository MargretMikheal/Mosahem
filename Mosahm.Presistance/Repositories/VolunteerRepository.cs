using Mosahm.Application.Interfaces.Repositories;
using Mosahm.Domain.Entities.Profiles;

namespace Mosahm.Persistence.Repositories
{
    public class VolunteerRepository : GenericRepository<Volunteer>, IVolunteerRepository
    {
        public VolunteerRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}