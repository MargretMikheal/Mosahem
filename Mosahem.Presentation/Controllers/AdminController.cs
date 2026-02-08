using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Application.Features.Admin.Commands.AddAdmin;
using mosahem.Application.Features.Admin.Commands.DeleteAdmin;
using mosahem.Presentation.Bases;
using Mosahem.Domain.AppMetaData;
using System.Security.Claims;

namespace Mosahem.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : MosahmControllerBase
    {
        [HttpPost(Router.AdminRouting.AddAdmin)]
        public async Task<IActionResult> AddAdmin([FromBody] AddAdminCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete(Router.AdminRouting.DeleteAdmin)]
        public async Task<IActionResult> DeleteAdmin(Guid id)
        {
            var currentUserIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(currentUserIdString, out Guid currentUserId))
                return Unauthorized();

            var command = new DeleteAdminCommand
            {
                AdminId = id,
                CurrentUserId = currentUserId
            };

            var response = await _mediator.Send(command);
            return NewResult(response);
        }
    }
}