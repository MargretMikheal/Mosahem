using Microsoft.EntityFrameworkCore;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.MasterData;

namespace mosahem.Persistence.Repositories
{
    public class FieldRepository : GenericRepository<Field>, IFieldRepository
    {
        public FieldRepository(MosahmDbContext dbContext) : base(dbContext) { }

        public async Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await GetTableNoTracking()
                .AnyAsync(f => f.NameAr == name || f.NameEn == name, cancellationToken);
        }
        public async Task<bool> IsExistByNameExcludeSelfAsync(Guid id, string? name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return await GetTableNoTracking()
                .AnyAsync(f => f.Id != id && (f.NameEn == name || f.NameAr == name), cancellationToken);
        }
    }
}