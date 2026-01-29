using Microsoft.EntityFrameworkCore;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Identity;

namespace mosahem.Persistence.Repositories
{
    public class UserRepository : GenericRepository<MosahmUser>, IUserRepository
    {
        private readonly DbSet<MosahmUser> _users;
        public UserRepository(MosahmDbContext dbContext) : base(dbContext)
        {
            _users = dbContext.Set<MosahmUser>();
        }
        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsPhoneUniqueAsync(string phone)
        {
            return !await _users.AnyAsync(u => u.PhoneNumber == phone);
        }
    }
}