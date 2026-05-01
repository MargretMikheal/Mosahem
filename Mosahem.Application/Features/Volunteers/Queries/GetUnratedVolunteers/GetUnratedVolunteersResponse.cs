namespace Mosahem.Application.Features.Volunteers.Queries.GetUnratedVolunteers
{
    public class GetUnratedVolunteersResponse
    {
        public Guid VolunteerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? ProfileImage { get; set; }
        public string? Bio { get; set; }
        public Guid OpportunityId { get; set; }
    }
}