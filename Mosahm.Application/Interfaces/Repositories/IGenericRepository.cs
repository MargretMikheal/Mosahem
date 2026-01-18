using Mosahm.Application.Interfaces.Repositories.Specifications;
using Mosahm.Domain.Entities;

namespace Mosahm.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        // CRUD Operations
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        // Advanced Specification Operations
        Task<IEnumerable<T>> FindAllAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<T?> FindFirstAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

        // Bulk Operations
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    }
}