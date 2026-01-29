using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Identity;

namespace mosahem.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<MosahmUser> 
    {
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> IsPhoneUniqueAsync(string phone);
    }
}