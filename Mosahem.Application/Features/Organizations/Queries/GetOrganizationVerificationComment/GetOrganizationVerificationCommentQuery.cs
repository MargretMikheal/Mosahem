using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationVerificationComment
{
    public class GetOrganizationVerificationCommentQuery : IRequest<Response<GetOrganizationVerificationCommentResponse>>
    {
        public Guid OrganizationId { get; set; }
    }
}
