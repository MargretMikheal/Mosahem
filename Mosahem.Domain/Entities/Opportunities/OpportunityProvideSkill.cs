using mosahem.Domain.Enums;

namespace mosahem.Domain.Entities.Opportunities
{
    public class OpportunityProvideSkill : OpportunitySkill
    {
        public OpportunityProvideSkill()
        {
            SkillType = OpportunitySkillType.Provide;
        }
    }
}
