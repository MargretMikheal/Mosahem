using Microsoft.EntityFrameworkCore.Storage;
using Mosahm.Application.Interfaces.Repositories;
using System.Collections;

namespace Mosahm.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MosahmDbContext _dbContext;
        private IDbContextTransaction? _currentTransaction;
        private Hashtable? _repositories;

        private IUserRepository? _users;
        private IVolunteerRepository? _volunteers;
        private IOrganizationRepository? _organizations;
        private IOpportunityRepository? _opportunities;
        private IOpportunityApplicationRepository? _opportunityApplications;
        private IOpportunityCommentRepository? _opportunityComments;
        private IFieldRepository? _fields;
        private ISkillRepository? _skills;
        private ICityRepository? _cities;
        private IGovernorateRepository? _governorates;
        private IQuestionRepository? _questions;
        private IQuestionAnswerRepository? _questionAnswers;

        public UnitOfWork(MosahmDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #region Specific Repositories
        public IUserRepository Users => _users ??= new UserRepository(_dbContext);
        public IVolunteerRepository Volunteers => _volunteers ??= new VolunteerRepository(_dbContext);
        public IOrganizationRepository Organizations => _organizations ??= new OrganizationRepository(_dbContext);
        public IOpportunityRepository Opportunities => _opportunities ??= new OpportunityRepository(_dbContext);
        public IOpportunityApplicationRepository OpportunityApplications => _opportunityApplications ??= new OpportunityApplicationRepository(_dbContext);
        public IOpportunityCommentRepository OpportunityComments => _opportunityComments ??= new OpportunityCommentRepository(_dbContext);
        public IFieldRepository Fields => _fields ??= new FieldRepository(_dbContext);
        public ISkillRepository Skills => _skills ??= new SkillRepository(_dbContext);
        public ICityRepository Cities => _cities ??= new CityRepository(_dbContext);
        public IGovernorateRepository Governorates => _governorates ??= new GovernorateRepository(_dbContext);
        public IQuestionRepository Questions => _questions ??= new QuestionRepository(_dbContext);
        public IQuestionAnswerRepository QuestionAnswers => _questionAnswers ??= new QuestionAnswerRepository(_dbContext);
        #endregion

        #region Generic Repository Accessor
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositoryType.MakeGenericType(typeof(TEntity)),
                    _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type]!;
        }
        #endregion

        #region Transaction Management
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null) return false;

            _currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            return true;
        }

        public async Task<bool> CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await SaveChangesAsync(cancellationToken);

                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync(cancellationToken);
                }
                return true;
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task<bool> RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.RollbackAsync(cancellationToken);
                }
                return true;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
            }

        }
        #endregion
    }
}