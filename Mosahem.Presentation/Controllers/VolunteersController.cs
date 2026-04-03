using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Volunteers.Commands.FollowOrganization;
using Mosahem.Domain.AppMetaData;
using Mosahem.Presentation.Filters;
using System.Security.Claims;

namespace Mosahem.Presentation.Controllers
{
    [Route(Router.VolunteerRouting.Prefix)]
    [ApiController]
    [Authorize(Roles = nameof(UserRole.Volunteer))]
    public class VolunteersController : MosahmControllerBase
    {
        [HttpPost(Router.VolunteerRouting.FollowOrganization)]
        [ValidateModelId]
        public async Task<IActionResult> FollowOrganization([FromRoute] Guid organizationId)
        {
            var volunteerIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(volunteerIdString) || !Guid.TryParse(volunteerIdString, out var volunteerId))
                return Unauthorized();

            var response = await _mediator.Send(new FollowOrganizationCommand
            {
                VolunteerId = volunteerId,
                OrganizationId = organizationId
            });

            return NewResult(response);
        }
    }
}
