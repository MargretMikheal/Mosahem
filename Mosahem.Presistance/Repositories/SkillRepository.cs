using Microsoft.EntityFrameworkCore;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities.MasterData;
using Mosahem.Application.Features.Skills.Queries.GetAllSkillsGrouped;
using Mosahem.Application.Interfaces.Repositories.Specifications;

namespace mosahem.Persistence.Repositories
{
    public class SkillRepository : GenericRepository<Skill>, ISkillRepository
    {
        public SkillRepository(MosahmDbContext dbContext) : base(dbContext) { }

        public async Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await GetTableNoTracking()
                .AnyAsync(s => s.NameEn == name || s.NameAr == name, cancellationToken);
        }
        public async Task<bool> IsExistByNameExcludeSelfAsync(Guid id, string? name, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return await GetTableNoTracking()
                .AnyAsync(s => s.Id != id && (s.NameEn == name || s.NameAr == name), cancellationToken);
        }
        public async Task<bool> AreAllExistingAsync(IReadOnlyCollection<Guid> skillIds, CancellationToken cancellationToken = default)
        {
            if (skillIds.Count == 0)
                return false;

            var count = await GetTableNoTracking()
                .CountAsync(s => skillIds.Contains(s.Id), cancellationToken);

            return count == skillIds.Count;
        }
        public async Task<IReadOnlyList<Skill>> GetAllWithFieldAsync(CancellationToken cancellationToken = default)
        {
            return await GetTableNoTracking()
                .Include(skill => skill.Field)
                .ToListAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<Skill>> GetAllForListingAsync(CancellationToken cancellationToken = default)
        {
            return await GetTableNoTracking()
                .Include(skill => skill.Field)
                .Where(skill => skill.Field != null)
                .OrderBy(skill => skill.Field.NameEn)
                .ThenBy(skill => skill.NameEn)
                .ToListAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<GetAllSkillsGroupedQueryResponse>> GetAllGroupedAsync(IReadOnlyList<Guid>? fieldIds, CancellationToken cancellationToken = default)
        {
            var specification = new Specification<Skill>()
                .Include(skill => skill.Field)
                .NoTracking();

            var skills = (await FindAllAsync(specification, cancellationToken)).ToList();
            var fieldOrder = BuildFieldOrder(fieldIds);

            return skills
                .Where(skill => skill.Field is not null)
                .GroupBy(skill => new
                {
                    skill.FieldId,
                    CategoryName = skill.Field!.Localize(skill.Field.NameAr, skill.Field.NameEn)
                })
                .Select(group => new GetAllSkillsGroupedQueryResponse
                {
                    FieldId = group.Key.FieldId,
                    Category = group.Key.CategoryName,
                    Skills = group
                        .Select(skill => new GetAllSkillsGroupedSkillResponse
                        {
                            Id = skill.Id,
                            Name = skill.Localize(skill.NameAr, skill.NameEn)
                        })
                        .OrderBy(skill => skill.Name)
                        .ToList()
                })
                .OrderBy(group => GetOrder(group.FieldId, fieldOrder))
                .ThenBy(group => group.Category)
                .ToList();
        }

        private static Dictionary<Guid, int> BuildFieldOrder(IReadOnlyList<Guid>? fieldIds)
        {
            var order = new Dictionary<Guid, int>();
            if (fieldIds is null)
                return order;

            var index = 0;
            foreach (var fieldId in fieldIds)
            {
                if (order.ContainsKey(fieldId))
                    continue;

                order[fieldId] = index;
                index++;
            }

            return order;
        }

        private static int GetOrder(Guid fieldId, IReadOnlyDictionary<Guid, int> fieldOrder)
        {
            return fieldOrder.TryGetValue(fieldId, out var order) ? order : int.MaxValue;
        }
    }
}