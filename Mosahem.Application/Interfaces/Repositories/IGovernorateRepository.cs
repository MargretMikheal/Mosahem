using mosahem.Domain.Entities.Location;

namespace mosahem.Application.Interfaces.Repositories
{
    public interface IGovernorateRepository : IGenericRepository<Governorate>
    {
        Task<IReadOnlyList<Governorate>> GetAllWithCitiesOrderedAsync(CancellationToken cancellationToken = default);
    }
}