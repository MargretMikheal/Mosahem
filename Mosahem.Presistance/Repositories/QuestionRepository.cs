using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.Questions;

namespace mosahem.Persistence.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}