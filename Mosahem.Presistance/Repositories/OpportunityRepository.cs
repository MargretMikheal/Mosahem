using Microsoft.EntityFrameworkCore;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Enums;
using Mosahem.Application.Interfaces.Repositories.Specifications;
using System.Linq.Expressions;

namespace mosahem.Persistence.Repositories
{
    public class OpportunityRepository : GenericRepository<Opportunity>, IOpportunityRepository
    {
        public OpportunityRepository(MosahmDbContext dbContext) : base(dbContext)
        { }
        public async Task<Opportunity?> GetOpportunityWithDetailsAsync(Guid opportunityId, CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Opportunity>(opportunity => opportunity.Id == opportunityId)
                .NoTracking()
                .AsSplitQuery()
                .Include("Organization.User")
                .Include("Address.City.Governorate")
                .Include("OpportunitySkills.Skill")
                .Include("OpportunityFields.Field")
                .Include("OpportunityLikes")
                .Include("OpportunityComments")
                .Include("OpportunitySaves")
                .Include("Questions");

            return await FindFirstAsync(spec, cancellationToken);
        }
        public async Task<(IReadOnlyList<Opportunity>, int totalCount)> GetOpportunitiesByVerificationStatusPageAsync(VerficationStatus verficationStatus, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Opportunity>(opportunity => opportunity.VerificationStatus == verficationStatus)
             .NoTracking()
            .OrderByDesc(opportunity => opportunity.CreatedAt);
            var totalCount = await CountAsync(spec, cancellationToken);
            spec = spec
                .Include("Organization.User")
                .Page((page - 1) * pageSize, pageSize);
            return ((await FindAllAsync(spec, cancellationToken)).ToList(), totalCount);
        }
        public async Task<bool> IsOwnedByOrganizationAsync(Guid opportunityId, Guid organizationId, CancellationToken cancellationToken = default)
        {
            var specification = new Specification<Opportunity>(
                opportunity => opportunity.Id == opportunityId && opportunity.OrganizationId == organizationId);

            return await CountAsync(specification, cancellationToken) > 0;
        }

        public async Task<string?> GetOpportunityPhotoKeyAsync(Guid opportunityId, CancellationToken cancellationToken)
        {
            return await GetTableNoTracking()
                .Where(o => o.Id == opportunityId).
                Select(o => o.PhotoKey)
                .FirstOrDefaultAsync();
        }

        public async Task<(IReadOnlyList<Opportunity>, int totalCount)> GetOrganizationOpportunitiesByVerificationStatusPageAsync(Guid organizationId, VerficationStatus verficationStatus, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Opportunity>(o => o.OrganizationId == organizationId && o.VerificationStatus == verficationStatus)
                .NoTracking()
                .OrderByAsc(o => o.CreatedAt);

            var totalCount = await CountAsync(spec, cancellationToken);

            spec = spec
                .Include("Organization.User")
                .Include("Address.City.Governorate")
                .Include("OpportunityLikes")
                .Include("OpportunityComments")
                .Include("OpportunitySaves")
                .Page((page - 1) * pageSize, pageSize);

            return ((await FindAllAsync(spec, cancellationToken)).ToList(), totalCount);
        }

        public async Task<(IReadOnlyList<Opportunity>, int totalCount)> GetOrganizationOpportunitiesByStatusPageAsync(Guid organizationId, OpportunityStatus status, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var spec = new Specification<Opportunity>(o => o.OrganizationId == organizationId && o.Status.HasFlag(status))
                .NoTracking()
                .OrderByAsc(o => o.CreatedAt);

            var totalCount = await CountAsync(spec, cancellationToken);

            spec = spec
                .Include("Organization.User")
                .Include("Address.City.Governorate")
                .Include("OpportunityLikes")
                .Include("OpportunityComments")
                .Include("OpportunitySaves")
                .Page((page - 1) * pageSize, pageSize);

            return ((await FindAllAsync(spec, cancellationToken)).ToList(), totalCount);
        }

        public async Task<(IReadOnlyList<Opportunity>, int totalCount)> GetAllOpportunitiesPageAsync(
            string? search,
            Guid? governateId,
            OpportunityStatus? status,
            OpportunityWorkType? workType,
            OpportunityLocationType? locationType,
            DateTime? startDate,
            List<Guid>? fieldIds,
            List<Guid>? requiredSkillIds,
            List<Guid>? providedSkillIds,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            #region Building Criteria
            Expression<Func<Opportunity, bool>> criteria = o =>

               (string.IsNullOrWhiteSpace(search) ||
               EF.Functions.Like(o.Title.ToLower(), $"{search.ToLower()}%") ||
               EF.Functions.Like(o.Descripition.ToLower(), $"{search.ToLower()}%")) &&

               (!governateId.HasValue ||
               (o.Address != null && o.Address.Any(a => a.City.GovernorateId == governateId.Value))) &&

               (!startDate.HasValue ||
               o.StartDate >= startDate) &&

               (fieldIds == null ||
               (o.OpportunityFields != null && o.OpportunityFields.Any(of => fieldIds.Contains(of.FieldId)))) &&

               (requiredSkillIds == null ||
               (o.OpportunitySkills != null && o.OpportunitySkills.Any(os => requiredSkillIds.Contains(os.SkillId) && os.SkillType == OpportunitySkillType.Require))) &&

               (providedSkillIds == null ||
                (o.OpportunitySkills != null && o.OpportunitySkills.Any(os => providedSkillIds.Contains(os.SkillId) && os.SkillType == OpportunitySkillType.Provide))) &&

                (!workType.HasValue ||
                o.WorkType.ToString() == workType.Value.ToString()) &&

                (!locationType.HasValue ||
                o.LocationType.ToString() == locationType.Value.ToString()) &&

                (!status.HasValue ||
                (o.Status & status.Value) == status.Value);

            #endregion
            var spec = new Specification<Opportunity>(criteria)
                .NoTracking()
                .AsSplitQuery()
                .Include("Organization.User")
                .Include("Address.City.Governorate")
                .Include("OpportunityLikes")
                .Include("OpportunityComments")
                .Include("OpportunitySaves")
                .OrderByAsc(o => o.StartDate)
                .Page((page - 1) * pageSize, pageSize);

            var totalCount = await CountAsync(spec, cancellationToken);

            return ((await FindAllAsync(spec, cancellationToken)).ToList(), totalCount);
        }
    }
}
