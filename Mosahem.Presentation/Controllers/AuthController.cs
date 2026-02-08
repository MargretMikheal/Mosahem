using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Application.Features.Authentication.Commands.ChangePassword;
using mosahem.Application.Features.Authentication.Commands.CompleteOrganizationRegistration;
using mosahem.Application.Features.Authentication.Commands.LoginUser;
using mosahem.Application.Features.Authentication.Commands.RevokeToken;
using mosahem.Application.Features.Authentication.Commands.SendEmailVerificationCode;
using mosahem.Application.Features.Authentication.Commands.SendRestPasswordOtp;
using mosahem.Application.Features.Authentication.Commands.ValidateBasicInfo;
using mosahem.Application.Features.Authentication.Commands.ValidateOrganizationLocations;
using mosahem.Application.Features.Authentication.Commands.VerifyEmail;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Authentication.Commands.RefreshTokens;
using Mosahem.Application.Features.Authentication.Commands.ResetPassword;
using Mosahem.Application.Features.Authentication.Commands.ValidateOrganizationFields;
using Mosahem.Application.Features.Authentication.Commands.VerifyRestPasswordOtp;
using Mosahem.Domain.AppMetaData;
using System.Security.Claims;

namespace Mosahem.Api.Controllers
{
    [ApiController]
    public class AuthController : MosahmControllerBase
    {
        #region Login & Management (Shared)

        [HttpPost(Router.AuthRouting.Login)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost(Router.AuthRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost(Router.AuthRouting.RevokeToken)]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        #endregion

        [HttpPost(Router.AuthRouting.ForgetPassword)]
        public async Task<IActionResult> ForgotPassword([FromBody] SendRestPasswordOtpCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost(Router.AuthRouting.VerifyOtp)]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyRestPasswordOtpCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost(Router.AuthRouting.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [Authorize]
        [HttpPut(Router.AuthRouting.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized();

            command.Id = userId;

            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost(Router.AuthRouting.SendEmailVerification)]
        public async Task<IActionResult> SendEmailVerification([FromBody] SendEmailVerificationCodeCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost(Router.AuthRouting.VerifyEmail)]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        #region Organization Registration Flow

        [HttpPost(Router.AuthRouting.OrganizationRegistration.ValidateBasicInfo)]
        public async Task<IActionResult> ValidateBasicInfo([FromBody] ValidateBasicInfoCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }


        [HttpPost(Router.AuthRouting.OrganizationRegistration.ValidateLocations)]
        public async Task<IActionResult> ValidateLocations([FromBody] ValidateOrganizationLocationsCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost(Router.AuthRouting.OrganizationRegistration.ValidateFields)]
        public async Task<IActionResult> ValidateFields([FromBody] ValidateOrganizationFieldsCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost(Router.AuthRouting.OrganizationRegistration.RegisterOrganization)]
        public async Task<IActionResult> RegisterOrganization([FromBody] CompleteOrganizationRegistrationCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        #endregion
    }
}