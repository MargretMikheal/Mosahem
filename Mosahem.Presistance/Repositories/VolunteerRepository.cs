using Microsoft.EntityFrameworkCore;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Profiles;

namespace mosahem.Persistence.Repositories
{
    public class VolunteerRepository : GenericRepository<Volunteer>, IVolunteerRepository
    {
        public VolunteerRepository(MosahmDbContext dbContext) : base(dbContext) { }
        public async Task<IReadOnlyList<Volunteer>> GetVolunteersWithProfilesAsync(CancellationToken cancellationToken = default)
        {
            return await GetTableNoTracking()
                .Include(v => v.User)
                .ToListAsync(cancellationToken);
        }
    }
}