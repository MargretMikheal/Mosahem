using Mosahm.Application.Interfaces.Repositories;
using Mosahm.Domain.Entities.MasterData;

namespace Mosahm.Persistence.Repositories
{
    public class FieldRepository : GenericRepository<Field>, IFieldRepository
    {
        public FieldRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}