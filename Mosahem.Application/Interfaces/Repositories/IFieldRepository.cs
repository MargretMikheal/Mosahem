using mosahem.Domain.Entities.MasterData;

namespace mosahem.Application.Interfaces.Repositories
{
    // Master Data / Lookups
    public interface IFieldRepository : IGenericRepository<Field>
    {
        Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken);
        Task<bool> IsExistByNameExcludeSelfAsync(Guid id, string? name, CancellationToken cancellationToken);
        Task<IReadOnlyList<Field>> GetAllOrderedAsync(CancellationToken cancellationToken = default);
    }
}