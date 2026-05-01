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
        public async Task<Volunteer?> GetVolunteerWithDetailsAsync(Guid volunteerId, CancellationToken cancellationToken = default)
        {
            return await GetTableNoTracking()
                .Include(v => v.User)
                .Include(v => v.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(c => c.Governorate)
                .Include(v => v.VolunteerSkills)
                .ThenInclude(vs => vs.Skill)
                .Include(v => v.VolunteerFields)
                .ThenInclude(vf => vf.Field)
                .Include(v => v.OpportunityApplications)
                .ThenInclude(oa => oa.Opportunity)
                .ThenInclude(o => o.Organization)
                .ThenInclude(org => org.User)
                .Include(v => v.OpportunityApplications)
                .ThenInclude(oa => oa.Opportunity)
                .ThenInclude(o => o.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(c => c.Governorate)
                .Include(v => v.OpportunitySaves)
                .ThenInclude(os => os.Opportunity)
                .ThenInclude(o => o.Organization)
                .ThenInclude(org => org.User)
                .Include(v => v.OpportunitySaves)
                .ThenInclude(os => os.Opportunity)
                .ThenInclude(o => o.Address)
                .ThenInclude(a => a.City)
                .ThenInclude(c => c.Governorate)
                .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);
        }
    }
}