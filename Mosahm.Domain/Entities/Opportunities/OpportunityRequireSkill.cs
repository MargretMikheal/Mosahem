using Mosahm.Domain.Enums;

namespace Mosahm.Domain.Entities.Opportunities
{
    public class OpportunityRequireSkill : OpportunitySkill
    {
        public OpportunityRequireSkill()
        {
            SkillType = OpportunitySkillType.Require;
        }
    }
}
