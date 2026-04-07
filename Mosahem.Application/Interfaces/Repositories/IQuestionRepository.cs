using mosahem.Domain.Entities.Questions;

namespace mosahem.Application.Interfaces.Repositories
{
    // Questions
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<bool> DoAllQuestionsExistForOpportunity(List<Guid> questionsIds, Guid opportunityId, CancellationToken cancellationToken);
        Task<IReadOnlyList<Question>> GetQuestionsByOpportunityIdAsync(Guid opportunityId, CancellationToken cancellationToken);
    }
}