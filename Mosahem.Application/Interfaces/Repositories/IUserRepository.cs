using mosahem.Domain.Entities.Identity;
using mosahem.Domain.Enums;

namespace mosahem.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<MosahmUser>
    {
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsPhoneUniqueAsync(string phone);
        Task<IReadOnlyList<MosahmUser>> GetUsersByRole(UserRole role, CancellationToken cancellationToken);
    }
}