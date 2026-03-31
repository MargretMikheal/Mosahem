namespace Mosahem.Application.Features.Admin.Queries.GetAllAdmins
{
    public class GetAllAdminsQueryResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
