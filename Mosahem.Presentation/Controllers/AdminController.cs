using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Application.Features.Admin.Commands.AddAdmin;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Admin.Commands.EditBasicInfo;
using Mosahem.Application.Features.Admin.Queries.GetAdminById;
using Mosahem.Application.Features.Admin.Queries.GetAllAdmins;
using Mosahem.Domain.AppMetaData;
using Mosahem.Presentation.Filters;
using System.Security.Claims;

namespace Mosahem.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class AdminController : MosahmControllerBase
    {
        #region Admin CRUDs
        [HttpPost(Router.AdminRouting.AddAdmin)]
        public async Task<IActionResult> AddAdmin([FromBody] AddAdminCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }


        [HttpPut(Router.AdminRouting.EditAdminInfo)]
        [ValidateModelId]
        public async Task<IActionResult> EditAdminInfo([FromBody] EditAdminInfoCommandRequest request)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized();
            var command = new EditAdminInfoCommand
            {
                AdminId = userId,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
            };
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(Router.AdminRouting.GetAdminById)]
        [ValidateModelId]
        public async Task<IActionResult> GetAdminById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetAdminByIdQuery(adminId: id));
            return NewResult(response);
        }
        [HttpGet(Router.AdminRouting.GetAllAdmins)]
        public async Task<IActionResult> GetAllAdmins()
        {
            var response = await _mediator.Send(new GetAllAdminsQuery());
            return NewResult(response);
        }
        #endregion
    }
}