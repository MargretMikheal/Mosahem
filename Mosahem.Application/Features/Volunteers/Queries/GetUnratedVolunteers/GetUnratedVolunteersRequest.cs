namespace Mosahem.Application.Features.Volunteers.Queries.GetUnratedVolunteers
{
    public class GetUnratedVolunteersRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
