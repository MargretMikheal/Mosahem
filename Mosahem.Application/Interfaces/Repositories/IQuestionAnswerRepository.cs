using mosahem.Domain.Entities.Questions;

namespace mosahem.Application.Interfaces.Repositories
{
    public interface IQuestionAnswerRepository : IGenericRepository<QuestionAnswer>
    {
        Task<IReadOnlyList<QuestionAnswer>> GetWithDetailsAsync(Guid volunteerId, Guid opportunityId, CancellationToken cancellation);
    }
}