using Microsoft.EntityFrameworkCore;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Identity;
using mosahem.Domain.Enums;

namespace mosahem.Persistence.Repositories
{
    public class UserRepository : GenericRepository<MosahmUser>, IUserRepository
    {
        public UserRepository(MosahmDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<MosahmUser>> GetUsersByRole(UserRole role, CancellationToken cancellationToken = default)
        {
            return await GetTableNoTracking()
                .Where(u => u.Role.Equals(role))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await _dbSet.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> IsPhoneUniqueAsync(string phone)
        {
            return !await _dbSet.AnyAsync(u => u.PhoneNumber == phone);
        }
    }
}