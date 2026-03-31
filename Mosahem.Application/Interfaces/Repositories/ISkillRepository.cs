using mosahem.Domain.Entities.MasterData;
using Mosahem.Application.Features.Skills.Queries.GetAllSkillsGrouped;

namespace mosahem.Application.Interfaces.Repositories
{
    public interface ISkillRepository : IGenericRepository<Skill>
    {
        Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken);
        Task<bool> IsExistByNameExcludeSelfAsync(Guid id, string? name, CancellationToken cancellationToken);
        Task<bool> AreAllExistingAsync(IReadOnlyCollection<Guid> skillIds, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Skill>> GetAllWithFieldAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Skill>> GetAllForListingAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<GetAllSkillsGroupedQueryResponse>> GetAllGroupedAsync(IReadOnlyList<Guid>? fieldIds, CancellationToken cancellationToken = default);
    }
}