using MediatR;
using mosahem.Application.Common;
using Mosahem.Application.Common.Pagination;

namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationVolunteersByVerificationStatus
{
    public class GetOrganizationVolunteersByVerificationStatusQuery : IRequest<Response<PaginatedResponse<GetOrganizationVolunteersByVerificationStatusResponse>>>
    {
        public Guid OrganizationId { get; set; }
        public string VerificationStatus { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
