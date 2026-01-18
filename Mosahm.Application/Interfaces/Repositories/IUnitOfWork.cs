using Mosahm.Domain.Entities;

namespace Mosahm.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        // Specific Repositories
        IUserRepository Users { get; }
        IVolunteerRepository Volunteers { get; }
        IOrganizationRepository Organizations { get; }
        IOpportunityRepository Opportunities { get; }
        IOpportunityApplicationRepository OpportunityApplications { get; }
        IOpportunityCommentRepository OpportunityComments { get; }
        IFieldRepository Fields { get; }
        ISkillRepository Skills { get; }
        ICityRepository Cities { get; }
        IGovernorateRepository Governorates { get; }
        IQuestionRepository Questions { get; }
        IQuestionAnswerRepository QuestionAnswers { get; }

        // Generic Accessor
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

        // Transaction Management
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<bool> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<bool> CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task<bool> RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}