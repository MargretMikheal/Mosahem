using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationLicense
{
    public class GetOrganizationLicenseQuery : IRequest<Response<GetOrganizationLicenseResponse>>
    {
        public Guid OrganizationId { get; set; }

        public GetOrganizationLicenseQuery(Guid organizationId)
        {
            OrganizationId = organizationId;
        }
    }
}
