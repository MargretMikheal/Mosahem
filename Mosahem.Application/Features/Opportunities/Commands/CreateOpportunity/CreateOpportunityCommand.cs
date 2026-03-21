using MediatR;
using mosahem.Application.Common;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Commands.CreateOpportunity
{
    public class CreateOpportunityCommand : IRequest<Response<Guid>>
    {
        public Guid OrganizationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? PhotoKey { get; set; }
        public OpportunityWorkType WorkType { get; set; }
        public OpportunityLocationType LocationType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfVolunteers { get; set; }

        public List<CreateOpportunityAddressDto>? Addresses { get; set; } = new();
        public List<Guid>? ProvidedSkillIds { get; set; } = new();
        public List<Guid>? RequiredSkillIds { get; set; } = new();
        public List<Guid>? FieldIds { get; set; } = new();

        public List<CreateOpportunityQuestionDto>? Questions { get; set; }
    }

    public class CreateOpportunityAddressDto
    {
        public Guid GovernorateId { get; set; }
        public Guid CityId { get; set; }
        public string? Description { get; set; }
    }

    public class CreateOpportunityQuestionDto
    {
        public Guid? QuestionId { get; set; }
        public string Description { get; set; }
        public AnswerType AnswerType { get; set; }
        public bool IsRequired { get; set; }
        public List<string>? Options { get; set; }
    }
}
