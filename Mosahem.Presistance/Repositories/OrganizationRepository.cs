using Microsoft.EntityFrameworkCore;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Profiles;
using mosahem.Domain.Enums;
using Mosahem.Application.Interfaces.Repositories.Specifications;

namespace mosahem.Persistence.Repositories
{
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(MosahmDbContext dbContext) : base(dbContext) { }
        public async Task<Organization?> GetOrganizationWithDetailsAsync(Guid organizationId, CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Organization>(o => o.Id == organizationId)
                .NoTracking()
                .AsSplitQuery()
                .Include(o => o.User)
                .Include(o => o.OrganizationFields)
                .Include("OrganizationFields.Field")
                .Include(o => o.Address)
                .Include("Address.City")
                .Include("Address.City.Governorate");

            return await FindFirstAsync(spec, cancellationToken);
        }
        public async Task<IReadOnlyList<Organization>> GetAllForListingAsync(CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Organization>()
                .NoTracking()
                .Include(o => o.User)
                .OrderByAsc(o => o.CreatedAt);

            return (await FindAllAsync(spec, cancellationToken)).ToList();
        }
        public Task<bool> ExistsAsync(Guid organizationId, CancellationToken cancellationToken = default)
        {
            return GetTableNoTracking().AnyAsync(o => o.Id == organizationId, cancellationToken);
        }

        public Task<bool> IsVerifiedAsync(Guid organizationId, CancellationToken cancellationToken = default)
        {
            return GetTableNoTracking()
                .AnyAsync(o => o.Id == organizationId && o.VerificationStatus == mosahem.Domain.Enums.VerficationStatus.Approved, cancellationToken);
        }

        public async Task<(IReadOnlyList<Organization>, int totalCount)> GetPendingOrganizationsPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Organization>(o => o.VerificationStatus.Equals(VerficationStatus.Pending))
                .NoTracking()
                .Include(o => o.User)
                .OrderByAsc(o => o.CreatedAt);

            int totalCount = await CountAsync(spec, cancellationToken);
            spec = spec.Page((pageNumber - 1) * pageSize, pageSize);

            return ((await FindAllAsync(spec, cancellationToken)).ToList(), totalCount);
        }

        public async Task<Organization?> GetOrganizationWithFieldsAsync(Guid organizationId, CancellationToken cancellationToken)
        {
            var spec = new Specification<Organization>(o => o.Id == organizationId)
                .Include(o => o.OrganizationFields);

            return (await FindFirstAsync(spec, cancellationToken));
        }
    }
}