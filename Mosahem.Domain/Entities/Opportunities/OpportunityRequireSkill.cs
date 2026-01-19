using mosahem.Domain.Enums;

namespace mosahem.Domain.Entities.Opportunities
{
    public class OpportunityRequireSkill : OpportunitySkill
    {
        public OpportunityRequireSkill()
        {
            SkillType = OpportunitySkillType.Require;
        }
    }
}
