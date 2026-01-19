using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Identity;

namespace mosahem.Persistence.Repositories
{
    // Identity
    public class UserRepository : GenericRepository<MosahmUser>, IUserRepository
    {
        public UserRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}