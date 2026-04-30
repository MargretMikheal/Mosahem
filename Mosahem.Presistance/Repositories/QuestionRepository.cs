using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Questions;
using Mosahem.Application.Interfaces.Repositories.Specifications;

namespace mosahem.Persistence.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(MosahmDbContext dbContext) : base(dbContext) { }

        public async Task<bool> DoAllQuestionsExistForOpportunity(List<Guid> questionsIds, Guid opportunityId, CancellationToken cancellationToken)
        {
            var opportunityQuestions = await GetQuestionsByOpportunityIdAsync(opportunityId, cancellationToken);

            return opportunityQuestions.All(q => questionsIds.Any(id => q.Id == id)) && opportunityQuestions.Count == questionsIds.Count;
        }

        public async Task<IReadOnlyList<Question>> GetQuestionsByOpportunityIdAsync(Guid opportunityId, CancellationToken cancellationToken)
        {
            var spec = new Specification<Question>(q => q.OpportunityId == opportunityId)
                .NoTracking();

            return (await FindAllAsync(spec, cancellationToken)).ToList();
        }
    }
}