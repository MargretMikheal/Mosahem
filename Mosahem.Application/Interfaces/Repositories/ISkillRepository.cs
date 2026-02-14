using mosahem.Domain.Entities.MasterData;

namespace mosahem.Application.Interfaces.Repositories
{
    public interface ISkillRepository : IGenericRepository<Skill>
    {
        Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken);
        Task<bool> IsExistByNameExcludeSelfAsync(Guid id, string? name, CancellationToken cancellationToken);

    }
}