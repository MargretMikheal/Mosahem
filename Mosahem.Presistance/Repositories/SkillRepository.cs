using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.MasterData;

namespace mosahem.Persistence.Repositories
{
    public class SkillRepository : GenericRepository<Skill>, ISkillRepository
    {
        public SkillRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}