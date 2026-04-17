using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Questions;
using Mosahem.Application.Interfaces.Repositories.Specifications;

namespace mosahem.Persistence.Repositories
{
    public class QuestionAnswerRepository : GenericRepository<QuestionAnswer>, IQuestionAnswerRepository
    {
        public QuestionAnswerRepository(MosahmDbContext dbContext) : base(dbContext) { }

        public async Task<IReadOnlyList<QuestionAnswer>> GetWithDetailsAsync(Guid volunteerId, Guid opportunityId, CancellationToken cancellation)
        {
            var spec = new Specification<QuestionAnswer>(qa => qa.VolunteerId == volunteerId && qa.Question.OpportunityId == opportunityId)
                .NoTracking()
                .Include(qa => qa.Question)
                .OrderByAsc(qa => qa.Question.Order);

            return (await FindAllAsync(spec, cancellation)).ToList();
        }
    }
}