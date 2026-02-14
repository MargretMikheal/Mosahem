using Microsoft.EntityFrameworkCore;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.MasterData;

namespace mosahem.Persistence.Repositories
{
    public class SkillRepository : GenericRepository<Skill>, ISkillRepository
    {
        public SkillRepository(MosahmDbContext dbContext) : base(dbContext) { }

        public async Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await GetTableNoTracking()
                .AnyAsync(s => s.NameEn == name || s.NameAr == name, cancellationToken);
        }
        public async Task<bool> IsExistByNameExcludeSelfAsync(Guid id, string? name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return await GetTableNoTracking()
                .AnyAsync(s => s.Id != id && (s.NameEn == name || s.NameAr == name), cancellationToken);
        }
    }
}