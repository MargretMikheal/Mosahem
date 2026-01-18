using Mosahm.Application.Interfaces.Repositories;
using Mosahm.Domain.Entities.Questions;

namespace Mosahm.Persistence.Repositories
{
    public class QuestionAnswerRepository : GenericRepository<QuestionAnswer>, IQuestionAnswerRepository
    {
        public QuestionAnswerRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}