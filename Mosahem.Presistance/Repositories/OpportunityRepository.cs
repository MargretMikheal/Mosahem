using Microsoft.EntityFrameworkCore;
using mosahem.Application.Common.Opportunities;
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
            await RefreshStatusesAsync(opportunity => opportunity.Id == opportunityId, cancellationToken);
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
                .Where(o => o.Id == opportunityId)
                .Select(o => o.PhotoKey)
                .FirstOrDefaultAsync();
        }

        public async Task<(IReadOnlyList<Opportunity>, int totalCount)> GetOrganizationOpportunitiesByVerificationStatusPageAsync(Guid organizationId, VerficationStatus verficationStatus, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            await RefreshStatusesAsync(o => o.OrganizationId == organizationId, cancellationToken);
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
            var normalizedSearch = search?.Trim().ToLower();
            var baseCriteria = BuildOpportunityCriteria(
                normalizedSearch,
                governateId,
                workType,
                locationType,
                startDate,
                fieldIds,
                requiredSkillIds,
                providedSkillIds,
                status: null);

            await RefreshStatusesAsync(baseCriteria, cancellationToken);

            var criteria = BuildOpportunityCriteria(
                normalizedSearch,
                governateId,
                workType,
                locationType,
                startDate,
                fieldIds,
                requiredSkillIds,
                providedSkillIds,
                status);

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

        private static Expression<Func<Opportunity, bool>> BuildOpportunityCriteria(
            string? normalizedSearch,
            Guid? governateId,
            OpportunityWorkType? workType,
            OpportunityLocationType? locationType,
            DateTime? startDate,
            List<Guid>? fieldIds,
            List<Guid>? requiredSkillIds,
            List<Guid>? providedSkillIds,
            OpportunityStatus? status)
        {
            var searchPattern = string.IsNullOrWhiteSpace(normalizedSearch)
                ? null
                : $"{normalizedSearch}%";

            return opportunity =>
                (searchPattern == null ||
                 EF.Functions.Like(opportunity.Title.ToLower(), searchPattern) ||
                 EF.Functions.Like(opportunity.Descripition.ToLower(), searchPattern)) &&
                (!governateId.HasValue ||
                 (opportunity.Address != null && opportunity.Address.Any(address => address.City.GovernorateId == governateId.Value))) &&
                (!startDate.HasValue ||
                 opportunity.StartDate >= startDate.Value) &&
                (fieldIds == null ||
                 (opportunity.OpportunityFields != null && opportunity.OpportunityFields.Any(field => fieldIds.Contains(field.FieldId)))) &&
                (requiredSkillIds == null ||
                 (opportunity.OpportunitySkills != null && opportunity.OpportunitySkills.Any(skill => requiredSkillIds.Contains(skill.SkillId) && skill.SkillType == OpportunitySkillType.Require))) &&
                (providedSkillIds == null ||
                 (opportunity.OpportunitySkills != null && opportunity.OpportunitySkills.Any(skill => providedSkillIds.Contains(skill.SkillId) && skill.SkillType == OpportunitySkillType.Provide))) &&
                (!workType.HasValue ||
                 opportunity.WorkType == workType.Value) &&
                (!locationType.HasValue ||
                 opportunity.LocationType == locationType.Value) &&
                (!status.HasValue ||
                 opportunity.Status.HasFlag(status.Value));
        }

        private async Task RefreshStatusesAsync(Expression<Func<Opportunity, bool>> criteria, CancellationToken cancellationToken)
        {
            var opportunities = await _dbContext.Opportunities
                .Where(criteria)
                .ToListAsync(cancellationToken);

            if (opportunities.Count == 0)
            {
                return;
            }

            var opportunityIds = opportunities
                .Select(opportunity => opportunity.Id)
                .ToList();

            var acceptedApplicantsCountByOpportunityId = await _dbContext.OpportunityApplications
                .AsNoTracking()
                .Where(application =>
                    opportunityIds.Contains(application.OpportunityId) &&
                    application.ApplicantStatus == ApplicantStatus.Accepted)
                .GroupBy(application => application.OpportunityId)
                .ToDictionaryAsync(
                    group => group.Key,
                    group => group.Count(),
                    cancellationToken);

            var hasChanges = false;
            foreach (var opportunity in opportunities)
            {
                acceptedApplicantsCountByOpportunityId.TryGetValue(opportunity.Id, out var acceptedApplicantsCount);
                hasChanges |= OpportunityStatusCalculator.TryApply(opportunity, acceptedApplicantsCount);
            }

            if (!hasChanges)
            {
                return;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
