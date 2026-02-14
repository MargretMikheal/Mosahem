using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Users.Queries.GetAllUsers;
using Mosahem.Application.Features.Users.Queries.GetUserInfo;
using Mosahem.Domain.AppMetaData;
using System.Security.Claims;

namespace Mosahem.Presentation.Controllers
{
    [Route(Router.UserRouting.Prefix)]
    [ApiController]
    [Authorize]
    public class UsersController : MosahmControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet(Router.UserRouting.AllUsers)]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _mediator.Send(new GetAllUsersQuery());
            return NewResult(response);
        }

        [HttpGet(Router.UserRouting.UserInfo)]
        public async Task<IActionResult> GetUserInfo()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized();

            var query = new GetUserInfoQuery { UserId = userId };
            var response = await _mediator.Send(query);
            return NewResult(response);
        }
    }
}
