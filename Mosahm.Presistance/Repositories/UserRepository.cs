using Mosahm.Application.Interfaces.Repositories;
using Mosahm.Domain.Entities.Identity;

namespace Mosahm.Persistence.Repositories
{
    // Identity
    public class UserRepository : GenericRepository<MosahmUser>, IUserRepository
    {
        public UserRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}