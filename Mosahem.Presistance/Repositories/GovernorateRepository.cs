using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Location;
using Mosahem.Application.Interfaces.Repositories.Specifications;

namespace mosahem.Persistence.Repositories
{
    public class GovernorateRepository : GenericRepository<Governorate>, IGovernorateRepository
    {
        public GovernorateRepository(MosahmDbContext dbContext) : base(dbContext)
        { }
        public async Task<IReadOnlyList<Governorate>> GetAllWithCitiesOrderedAsync(CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Governorate>()
                .NoTracking()
                .AsSplitQuery()
                .OrderByAsc(g => g.NameEn)
                .Include(g => g.Cities);

            return (await FindAllAsync(spec, cancellationToken)).ToList();
        }
    }
}
