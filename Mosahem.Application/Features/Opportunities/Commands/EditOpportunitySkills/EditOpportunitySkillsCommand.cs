using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunitySkills
{
    public class EditOpportunitySkillsCommand : IRequest<Response<string>>
    {
        public EditOpportunitySkillsCommand(Guid opportunityId, Guid organizationId, string skillType, HashSet<Guid> skillsIds)
        {
            OpportunityId = opportunityId;
            OrganizationId = organizationId;
            SkillType = skillType;
            SkillsIds = skillsIds;
        }

        public Guid OpportunityId { get; set; }
        public Guid OrganizationId { get; set; }
        public string SkillType { get; set; }
        public HashSet<Guid> SkillsIds { get; set; }
    }
}
