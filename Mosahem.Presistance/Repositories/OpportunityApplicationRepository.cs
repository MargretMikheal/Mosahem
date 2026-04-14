using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Entities.Profiles;
using mosahem.Domain.Enums;
using Mosahem.Application.Interfaces.Repositories.Specifications;

namespace mosahem.Persistence.Repositories
{
    public class OpportunityApplicationRepository : GenericRepository<OpportunityApplication>, IOpportunityApplicationRepository
    {
        public OpportunityApplicationRepository(MosahmDbContext dbContext) : base(dbContext) { }

        public async Task<int> GetAcceptedApplicantsCountAsync(Guid opportunityId, CancellationToken cancellationToken)
        {
            var spec = new Specification<OpportunityApplication>(oa => oa.OpportunityId == opportunityId && oa.ApplicantStatus == ApplicantStatus.Accepted)
                .NoTracking();

            return await CountAsync(spec, cancellationToken);
        }


        public async Task<bool> IsExistAsync(Guid volunteerId, Guid opportunityId, CancellationToken cancellationToken)
        {
            var spec = new Specification<OpportunityApplication>(oa => oa.VolunteerId == volunteerId && oa.OpportunityId == opportunityId)
                .NoTracking();

            return (await FindFirstAsync(spec, cancellationToken)) is not null;
        }
        public async Task<(IReadOnlyList<Volunteer>, int totalCount)> GetApplicantsByStatusPageAsync(Guid opportunityId, ApplicantStatus status, int page, int pageSize, CancellationToken cancellationToken)
        {
            var spec = new Specification<OpportunityApplication>(oa => oa.OpportunityId == opportunityId && oa.ApplicantStatus == status)
                 .NoTracking()
                 .OrderByDesc(oa => oa.CreatedAt);

            var totalCount = await CountAsync(spec, cancellationToken);

            spec = spec
                .Include(oa => oa.Volunteer)
                .Include("Volunteer.User")
                 .Page((page - 1) * pageSize, pageSize);


            return ((await FindAllAsync(spec, cancellationToken)).Select(oa => oa.Volunteer).ToList(), totalCount);
        }

        public async Task<OpportunityApplication?> GetByVolunteerAndOpportunityIdAsync(Guid volunteerId, Guid opportunityId, CancellationToken cancellationToken)
        {
            var spec = new Specification<OpportunityApplication>(oa => oa.VolunteerId == volunteerId && oa.OpportunityId == opportunityId)
                .Include(oa => oa.Volunteer)
                .Include("Volunteer.User")
                .Include(oa => oa.Opportunity);

            return await FindFirstAsync(spec, cancellationToken);
        }
    }
}