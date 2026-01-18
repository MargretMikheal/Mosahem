using Mosahm.Application.Interfaces.Repositories;
using Mosahm.Domain.Entities.Questions;

namespace Mosahm.Persistence.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(MosahmDbContext dbContext) : base(dbContext) { }
    }
}