using Microsoft.EntityFrameworkCore;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Location;

namespace mosahem.Persistence.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(MosahmDbContext dbContext) : base(dbContext) { }
        public async Task<IReadOnlyList<City>> GetCitiesByGovernate(Guid governateId, CancellationToken cancellationToken = default)
        {
            return await GetTableNoTracking()
                .Where(c => c.GovernorateId == governateId)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsExistByGovernateAsync(Guid governateId, Guid cityId, CancellationToken cancellationToken)
        {
            return await GetTableNoTracking()
                .AnyAsync(c =>
                c.Id == cityId &&
                c.GovernorateId == governateId,
                cancellationToken);
        }

        public Task<bool> IsExistByNameInGovernateAsync(Guid governateId, string name, CancellationToken cancellationToken = default)
        {
            return GetTableNoTracking()
                 .AnyAsync(c => c.GovernorateId == governateId && (c.NameAr == name || c.NameEn == name), cancellationToken);
        }
        public async Task<bool> AreValidCityGovernoratePairsAsync(
            IReadOnlyCollection<Guid> cityIds,
            IReadOnlyDictionary<Guid, Guid> cityGovernoratePairs,
            CancellationToken cancellationToken = default)
        {
            if (cityIds.Count == 0 || cityGovernoratePairs.Count == 0)
                return false;

            var cities = await GetTableNoTracking()
                .Where(c => cityIds.Contains(c.Id))
                .Select(c => new { c.Id, c.GovernorateId })
                .ToListAsync(cancellationToken);

            if (cities.Count != cityIds.Count)
                return false;

            return cities.All(c => cityGovernoratePairs.TryGetValue(c.Id, out var governorateId)
                                   && governorateId == c.GovernorateId);
        }
    }
}