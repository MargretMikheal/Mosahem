using Mosahm.Domain.Enums;

namespace Mosahm.Domain.Entities
{
    public class OpportunityProvideSkill : OpportunitySkill
    {
        public OpportunityProvideSkill()
        {
            SkillType = OpportunitySkillType.Provide;
        }
    }
}
