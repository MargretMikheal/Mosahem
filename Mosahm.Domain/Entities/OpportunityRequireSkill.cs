using Mosahm.Domain.Enums;

namespace Mosahm.Domain.Entities
{
    public class OpportunityRequireSkill : OpportunitySkill
    {
        public OpportunityRequireSkill()
        {
            SkillType = OpportunitySkillType.Require;
        }
    }
}
