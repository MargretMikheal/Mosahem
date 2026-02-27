using mosahem.Domain.Entities.Location;

namespace mosahem.Application.Interfaces.Repositories
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<bool> IsExistByNameInGovernateAsync(Guid governateId, string name, CancellationToken cancellationToken);
        Task<bool> IsExistByGovernateAsync(Guid governateId, Guid cityId, CancellationToken cancellationToken);
        Task<IReadOnlyList<City>> GetCitiesByGovernate(Guid governateId, CancellationToken cancellationToken);
    }
}