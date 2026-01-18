using Mosahm.Application.Interfaces.Repositories;
using Mosahm.Domain.Entities.MasterData;

namespace Mosahm.Persistence.Repositories
{
    public class SkillRepository : GenericRepository<Skill>, ISkillRepository
    {
        public SkillRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}