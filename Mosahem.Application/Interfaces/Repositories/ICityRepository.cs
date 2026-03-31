using mosahem.Domain.Entities.Location;

namespace mosahem.Application.Interfaces.Repositories
{
    public interface ICityRepository : IGenericRepository<City>
    {
        Task<bool> IsExistByNameInGovernateAsync(Guid governateId, string name, CancellationToken cancellationToken);
        Task<IReadOnlyList<City>> GetCitiesByGovernate(Guid GovernateId, CancellationToken cancellationToken);
        Task<bool> AreValidCityGovernoratePairsAsync(IReadOnlyCollection<Guid> cityIds, IReadOnlyDictionary<Guid, Guid> cityGovernoratePairs, CancellationToken cancellationToken = default);
        Task<bool> IsExistByGovernateAsync(Guid governateId, Guid cityId, CancellationToken cancellationToken);
    }
}