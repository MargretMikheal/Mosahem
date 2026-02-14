using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Presentation.Bases;
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
