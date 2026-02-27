using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Addresses.Commands.Organization.AddOrganizationAddress;
using Mosahem.Application.Features.Addresses.Commands.Organization.DeleteOrganizationAddress;
using Mosahem.Application.Features.Addresses.Commands.Organization.EditOrganizationAddress;
using Mosahem.Domain.AppMetaData;
using Mosahem.Presentation.Filters;
using System.Security.Claims;

namespace Mosahem.Presentation.Controllers
{
    [Route(Router.AddressRouting.Prefix)]
    [ApiController]
    public class AddressController : MosahmControllerBase
    {
        #region Organization
        [Authorize(Roles = nameof(UserRole.Organization))]
        [HttpPost(Router.AddressRouting.AddOrganizationAddress)]
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
        [HttpDelete(Router.AddressRouting.DeleteOrganizationAddress)]
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
        [HttpPut(Router.AddressRouting.EditOrganizationAddress)]
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
