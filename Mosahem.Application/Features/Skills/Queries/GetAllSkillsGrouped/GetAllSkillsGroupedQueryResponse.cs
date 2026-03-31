namespace Mosahem.Application.Features.Skills.Queries.GetAllSkillsGrouped
{
    public class GetAllSkillsGroupedQueryResponse
    {
        public Guid FieldId { get; set; }
        public string Category { get; set; }
        public IReadOnlyList<GetAllSkillsGroupedSkillResponse> Skills { get; set; }
    }

    public class GetAllSkillsGroupedSkillResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
