using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Addresses.Commands.Organization.AddOrganizationAddress;
using Mosahem.Application.Features.Addresses.Commands.Organization.DeleteOrganizationAddress;
using Mosahem.Application.Features.Addresses.Commands.Organization.EditOrganizationAddress;
using Mosahem.Application.Features.Addresses.Queries.GetOrganizationLocations;
using Mosahem.Domain.AppMetaData;
using Mosahem.Presentation.Filters;
using System.Security.Claims;

namespace Mosahem.Presentation.Controllers
{
    [ApiController]
    public class AddressController : MosahmControllerBase
    {
        #region Organization
        [HttpGet(Router.OrganizationRouting.Locations)]
        public async Task<IActionResult> GetOrganizationLocations(Guid id)
        {
            var response = await _mediator.Send(new GetOrganizationLocationsQuery { OrganizationId = id });
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Organization))]
        [HttpPost(Router.OrganizationRouting.AddOrganizationAddress)]
        [ValidateModelId]
        public async Task<IActionResult> AddOrganizationAddress([FromBody] AddOrganizationAddressCommandRequest request)
        {
            var orgIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(orgIdString) || !Guid.TryParse(orgIdString, out Guid organizationId))
                return Unauthorized();

            var command = new AddOrganizationAddressCommand
            {
                OrganizationId = organizationId,
                GovernateId = request.GovernateId,
                CityID = request.CityID,
                Description = request.Description
            };

            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Organization))]
        [HttpDelete(Router.OrganizationRouting.DeleteOrganizationAddress)]
        [ValidateModelId]
        public async Task<IActionResult> DeleteOrganizationAddress([FromRoute] Guid id)
        {
            var orgIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(orgIdString) || !Guid.TryParse(orgIdString, out Guid organizationId))
                return Unauthorized();

            var response = await _mediator.Send(new DeleteOrganizationAddressCommand(organizationId, addressId: id));
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Organization))]
        [HttpPut(Router.OrganizationRouting.EditOrganizationAddress)]
        [ValidateModelId]
        public async Task<IActionResult> EditOrganizationAddress(
            [FromRoute] Guid id,
            [FromBody] EditOrganizationAddressRequest request)
        {
            var orgIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(orgIdString) || !Guid.TryParse(orgIdString, out Guid organizationId))
                return Unauthorized();

            var response = await _mediator.Send(new EditOrganizationAddressCommand
            {
                OrganizationId = organizationId,
                GovernateId = request.GovernateId,
                AddressId = id,
                CityId = request.CityId,
                Description = request.Description,
            });
            return NewResult(response);
        }
        #endregion
    }
}
