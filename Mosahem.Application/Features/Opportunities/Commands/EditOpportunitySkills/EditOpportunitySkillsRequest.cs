namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunitySkills
{
    public class EditOpportunitySkillsRequest
    {
        public EditOpportunitySkillsRequest(string skillType, HashSet<Guid> skillsIds)
        {
            SkillType = skillType;
            SkillsIds = skillsIds;
        }

        public string SkillType { get; set; }
        public HashSet<Guid> SkillsIds { get; set; }
    }
}
