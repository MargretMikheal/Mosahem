namespace Mosahem.Application.Features.Opportunities.Queries.GetOpportunityById
{
    public class OpportunityDetailsResponse
    {
        public Guid OpportunityId { get; set; }
        public string OpportunityName { get; set; } = string.Empty;
        public string OpportunityDescription { get; set; } = string.Empty;
        public string? OpportunityPhotoUrl { get; set; }
        public List<string> OpportunityStatus { get; set; } = new();
        public string VerificationStatus { get; set; } = string.Empty;
        public string WorkType { get; set; } = string.Empty;
        public string LocationType { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfVolunteers { get; set; }
        public int ApplicantsCount { get; set; }
        public OpportunityOrganizationResponse Organization { get; set; } = new();
        public List<OpportunityLocationResponse> Locations { get; set; } = new();
        public List<OpportunitySkillResponse> RequiredSkills { get; set; } = new();
        public List<OpportunitySkillResponse> ProvidedSkills { get; set; } = new();
        public List<OpportunityFieldResponse> Fields { get; set; } = new();
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public int SavesCount { get; set; }
        public List<OpportunityQuestionResponse> Questions { get; set; } = new();
    }

    public class OpportunityOrganizationResponse
    {
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string? OrganizationLogoUrl { get; set; }
    }

    public class OpportunityLocationResponse
    {
        public Guid CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public Guid GovernorateId { get; set; }
        public string GovernorateName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class OpportunitySkillResponse
    {
        public Guid SkillId { get; set; }
        public string SkillName { get; set; } = string.Empty;
    }
    public class OpportunityFieldResponse
    {
        public Guid FieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
    }

    public class OpportunityQuestionResponse
    {
        public Guid QuestionId { get; set; }
        public int Order { get; set; }
        public string Description { get; set; } = string.Empty;
        public string AnswerType { get; set; } = string.Empty;
        public bool IsRequired { get; set; }
        public List<string> Options { get; set; } = new();
    }
}
