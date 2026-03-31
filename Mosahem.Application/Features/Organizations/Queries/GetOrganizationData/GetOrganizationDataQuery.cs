using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationData
{
    public class GetOrganizationDataQuery : IRequest<Response<GetOrganizationDataResponse>>
    {
        public Guid OrganizationId { get; set; }
    }
}
