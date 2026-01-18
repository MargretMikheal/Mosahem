using Mosahm.Domain.Entities.MasterData;
using Mosahm.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mosahm.Domain.Entities.Opportunities
{
    public abstract class OpportunitySkill
    {
        public Guid Id { get; set; }
        public Guid OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }

        public Guid SkillId { get; set; }
        public Skill Skill { get; set; }

        public OpportunitySkillType SkillType { get; protected set; }
    }

}
