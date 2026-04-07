using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Volunteers.Commands.EditVolunteerFields;
using Mosahem.Application.Features.Volunteers.Commands.EditVolunteerSkills;
using Mosahem.Application.Features.Volunteers.Commands.Location.DeleteVolunteerAddress;
using Mosahem.Application.Features.Volunteers.Commands.Location.EditVolunteerAddress;
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
        public async Task<IActionResult> EditAddress([FromBody] EditVolunteerAddressRequest request)
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
        public async Task<IActionResult> DeleteAddress()
        {
            var volunteerIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(volunteerIdString) || !Guid.TryParse(volunteerIdString, out Guid volunteerId))
                return Unauthorized();

            var response = await _mediator.Send(new DeleteVolunteerAddressCommand(volunteerId));
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Volunteer))]
        [HttpPut(Router.VolunteerRouting.EditSkills)]
        [ValidateModelId]
        public async Task<IActionResult> EditSkills([FromBody] EditVolunteerSkillsRequest request)
        {
            var volunteerIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(volunteerIdString) || !Guid.TryParse(volunteerIdString, out Guid volunteerId))
                return Unauthorized();

            var response = await _mediator.Send(new EditVolunteerSkillsCommand(volunteerId, request.SkillsIds));
            return NewResult(response);

        }

    }
}
