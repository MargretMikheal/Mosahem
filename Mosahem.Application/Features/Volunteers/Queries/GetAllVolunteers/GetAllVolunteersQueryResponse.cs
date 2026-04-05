namespace Mosahem.Application.Features.Volunteers.Queries.GetAllVolunteers
{
    public class GetAllVolunteersQueryResponse
    {
        public string? ProfileImage { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Bio { get; set; }
    }
}
