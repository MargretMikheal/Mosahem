namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationVerificationComment
{
    public class GetOrganizationVerificationCommentResponse
    {
        public bool IsRejected { get; set; }
        public string? VerificationComment { get; set; }
    }
}
