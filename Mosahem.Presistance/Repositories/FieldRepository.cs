using Microsoft.EntityFrameworkCore;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.MasterData;

namespace mosahem.Persistence.Repositories
{
    public class FieldRepository : GenericRepository<Field>, IFieldRepository
    {
        public FieldRepository(MosahmDbContext dbContext) : base(dbContext) { }

        public async Task<bool> IsExistByNameAsync(string name)
        {
            return await GetTableNoTracking()
                .AnyAsync(f => f.NameAr == name || f.NameEn == name);
        }
    }
}