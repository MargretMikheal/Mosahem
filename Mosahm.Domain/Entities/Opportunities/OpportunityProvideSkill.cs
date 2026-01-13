using Mosahm.Domain.Enums;

namespace Mosahm.Domain.Entities.Opportunities
{
    public class OpportunityProvideSkill : OpportunitySkill
    {
        public OpportunityProvideSkill()
        {
            SkillType = OpportunitySkillType.Provide;
        }
    }
}
