using Microsoft.EntityFrameworkCore;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.MasterData;
using Mosahem.Application.Interfaces.Repositories.Specifications;

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

        public async Task<IReadOnlyList<Field>> GetAllOrderedAsync(CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Field>()
                .NoTracking()
                .OrderByAsc(field => field.NameEn);

            return (await FindAllAsync(spec, cancellationToken)).ToList();
        }
    }
}