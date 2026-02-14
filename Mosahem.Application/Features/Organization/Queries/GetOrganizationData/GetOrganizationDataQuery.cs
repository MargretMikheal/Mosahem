using MediatR;
using mosahem.Application.Common;
using Mosahem.Application.Features.Organization.Queries.GetOrganizationData.Mosahem.Application.Features.Organization.Queries.GetOrganizationData;

namespace Mosahem.Application.Features.Organization.Queries.GetOrganizationData
{
    public class GetOrganizationDataQuery : IRequest<Response<GetOrganizationDataResponse>>
    {
        public Guid OrganizationId { get; set; }
    }
}
