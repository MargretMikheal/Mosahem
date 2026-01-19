using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Questions;

namespace mosahem.Persistence.Repositories
{
    public class QuestionAnswerRepository : GenericRepository<QuestionAnswer>, IQuestionAnswerRepository
    {
        public QuestionAnswerRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}