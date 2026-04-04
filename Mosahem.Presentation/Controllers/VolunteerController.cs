using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Addresses.Commands.EditVolunteerAddress;
using Mosahem.Application.Features.Volunteers.Commands.DeleteVolunteerAddress;
using Mosahem.Application.Features.Volunteers.Commands.EditVolunteerFields;
using Mosahem.Domain.AppMetaData;
using Mosahem.Presentation.Filters;
using System.Security.Claims;

namespace Mosahem.Presentation.Controllers
{
    [ApiController]
    public class VolunteerController : MosahmControllerBase
    {
        [HttpPut(Router.VolunteerRouting.EditFields)]
        [Authorize(Roles = nameof(UserRole.Volunteer))]
        public async Task<IActionResult> EditFields([FromBody] EditVolunteerFieldsRequest request)
        {
            var volunteerIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(volunteerIdString) || !Guid.TryParse(volunteerIdString, out Guid volunteerId))
                return Unauthorized();
            var response = await _mediator.Send(new EditVolunteerFieldsCommand(volunteerId, request.FieldsIds));
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Volunteer))]
        [HttpPut(Router.VolunteerRouting.EditAddress)]
        [ValidateModelId]
        public async Task<IActionResult> EditVolunteerAddress([FromBody] EditVolunteerAddressRequest request)
        {
            var volunteerIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(volunteerIdString) || !Guid.TryParse(volunteerIdString, out Guid volunteerId))
                return Unauthorized();

            var response = await _mediator.Send(new EditVolunteerAddressCommand
            {
                VolunteerId = volunteerId,
                GovernateId = request.GovernateId,
                CityId = request.CityId,
                Description = request.Description,
            });
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Volunteer))]
        [HttpDelete(Router.VolunteerRouting.DeleteAddress)]
        [ValidateModelId]
        public async Task<IActionResult> DeleteVolunteerAddress()
        {
            var volunteerIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(volunteerIdString) || !Guid.TryParse(volunteerIdString, out Guid volunteerId))
                return Unauthorized();

            var response = await _mediator.Send(new DeleteVolunteerAddressCommand(volunteerId));
            return NewResult(response);
        }
    }
}
