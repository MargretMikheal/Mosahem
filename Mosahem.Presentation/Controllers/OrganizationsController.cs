using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Organization.Queries.GetAllOrganizations;
using Mosahem.Application.Features.Organization.Queries.GetOrganizationData;
using Mosahem.Application.Features.Organization.Queries.GetOrganizationFields;
using Mosahem.Application.Features.Organization.Queries.GetOrganizationLocations;
using Mosahem.Domain.AppMetaData;
using System.Security.Claims;

namespace Mosahem.Presentation.Controllers
{
    [Route(Router.OrganizationRouting.Prefix)]
    [ApiController]
    public class OrganizationsController : MosahmControllerBase
    {
        [HttpGet(Router.OrganizationRouting.AllOrganizations)]
        public async Task<IActionResult> GetAllOrganizations()
        {
            var response = await _mediator.Send(new GetAllOrganizationsQuery());
            return NewResult(response);
        }

        [Authorize(Roles = "Organization")]
        [HttpGet(Router.OrganizationRouting.OrganizationData)]
        public async Task<IActionResult> GetOrganizationData()
        {
            var organizationIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(organizationIdString) || !Guid.TryParse(organizationIdString, out Guid organizationId))
                return Unauthorized();

            var response = await _mediator.Send(new GetOrganizationDataQuery { OrganizationId = organizationId });
            return NewResult(response);
        }

        [HttpGet(Router.OrganizationRouting.Fields)]
        public async Task<IActionResult> GetOrganizationFields(Guid id)
        {
            var response = await _mediator.Send(new GetOrganizationFieldsQuery { OrganizationId = id });
            return NewResult(response);
        }

        [HttpGet(Router.OrganizationRouting.Locations)]
        public async Task<IActionResult> GetOrganizationLocations(Guid id)
        {
            var response = await _mediator.Send(new GetOrganizationLocationsQuery { OrganizationId = id });
            return NewResult(response);
        }
    }
}
