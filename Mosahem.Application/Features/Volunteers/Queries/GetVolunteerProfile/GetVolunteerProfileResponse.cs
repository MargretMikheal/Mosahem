using Mosahem.Application.Features.Opportunities.Queries.GetOpportunityById;

namespace Mosahem.Application.Features.Volunteers.Queries.GetVolunteerProfile
{
    public class GetVolunteerProfileResponse
    {
        public Guid VolunteerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ProfilePhoto { get; set; }
        public string? CoverPhoto { get; set; }
        public string? Bio { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Location { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int TotalHours { get; set; }
        public int CompletedOpportunitiesCount { get; set; }
        public List<VolunteerOpportunityResponse> CompletedOpportunities { get; set; } = new();
        public List<VolunteerMasterDataResponse> Skills { get; set; } = new();
        public List<VolunteerMasterDataResponse> Fields { get; set; } = new();
        public List<VolunteerOpportunityResponse> SavedOpportunities { get; set; } = new();
    }

    public class VolunteerMasterDataResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class VolunteerOpportunityResponse
    {
        public Guid OpportunityId { get; set; }
        public string OpportunityName { get; set; } = string.Empty;
        public string OpportunityDescription { get; set; } = string.Empty;
        public string? OpportunityPhotoUrl { get; set; }
        public string WorkType { get; set; } = string.Empty;
        public string LocationType { get; set; } = string.Empty;
        public List<string> OpportunityStatus { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Vacancies { get; set; }
        public int ApplicantsCount { get; set; }
        public OpportunityOrganizationResponse Organization { get; set; } = new();
        public List<OpportunityLocationResponse> Locations { get; set; } = new();
    }
}
