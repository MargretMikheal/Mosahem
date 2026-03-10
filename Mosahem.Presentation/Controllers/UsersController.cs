using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Application.Features.Authentication.Commands.ChangePassword;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Users.Commands.ChangeEmail.ChangeEmailOtpVerification;
using Mosahem.Application.Features.Users.Commands.ChangeEmail.SendChangeEmailOtp;
using Mosahem.Application.Features.Users.Commands.ChangeUserEmail.ChangeEmail;
using Mosahem.Application.Features.Users.Queries.GetAllUsers;
using Mosahem.Application.Features.Users.Queries.GetUserInfo;
using Mosahem.Domain.AppMetaData;
using Mosahem.Presentation.Filters;
using System.Security.Claims;

namespace Mosahem.Presentation.Controllers
{
    [Route(Router.UserRouting.Prefix)]
    [ApiController]
    [Authorize]
    public class UsersController : MosahmControllerBase
    {
        [Authorize(Roles = nameof(UserRole.Admin))]
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
        [Authorize]
        [HttpPut(Router.UserRouting.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized();

            command.Id = userId;

            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        #region Change Email
        [Authorize]
        [ValidateModelId]
        [HttpPost(Router.UserRouting.SendChangeEmailOtp)]
        public async Task<IActionResult> SendChangeEmailOtp([FromBody] SendChangeEmailOtpRequest request)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                 ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized();

            var response = await _mediator.Send(new SendChangeEmailOtp
            {
                UserId = userId,
                Email = request.Email,
            });
            return NewResult(response);
        }

        [Authorize]
        [ValidateModelId]
        [HttpPost(Router.UserRouting.ChangeEmailOtpVerification)]
        public async Task<IActionResult> ChangeEmailOtpVerification([FromBody] ChangeEmailOtpVerificationRequest request)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized();

            var response = await _mediator.Send(new ChangeEmailOtpVerification
            {
                UserId = userId,
                Email = request.Email,
                Code = request.Code
            });
            return NewResult(response);
        }

        [Authorize]
        [ValidateModelId]
        [HttpPut(Router.UserRouting.ChangeEmail)]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailCommandRequest request)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized();

            var response = await _mediator.Send(new ChangeEmailCommand
            {
                UserId = userId,
                Email = request.Email,
                Code = request.Code
            });
            return NewResult(response);
        }
        #endregion
    }
}
