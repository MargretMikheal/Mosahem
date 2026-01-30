using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Application.Features.Authentication.Commands.ChangePassword;
using mosahem.Application.Features.Authentication.Commands.CompleteOrganizationRegistration;
using mosahem.Application.Features.Authentication.Commands.LoginUser;
using mosahem.Application.Features.Authentication.Commands.RevokeToken;
using mosahem.Application.Features.Authentication.Commands.SendEmailVerificationCode;
using mosahem.Application.Features.Authentication.Commands.SendOtp;
using mosahem.Application.Features.Authentication.Commands.ValidateBasicInfo;
using mosahem.Application.Features.Authentication.Commands.ValidateOrganizationLocations;
using mosahem.Application.Features.Authentication.Commands.VerifyEmail;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Authentication.Commands.RefreshTokens;
using Mosahem.Application.Features.Authentication.Commands.ResetPassword;
using Mosahem.Application.Features.Authentication.Commands.ValidateOrganizationFields;
using Mosahem.Application.Features.Authentication.Commands.VerifyOtp;
using System.Security.Claims;

namespace Mosahem.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : MosahmControllerBase
    {
        #region Login & Management (Shared)

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        #endregion

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] SendOtpCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized();

            command.Id = userId;

            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost("send-email-verification")]
        public async Task<IActionResult> SendEmailVerification([FromBody] SendEmailVerificationCodeCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        #region Organization Registration Flow

        [HttpPost("organization/validate-basic-info")]
        public async Task<IActionResult> ValidateBasicInfo([FromBody] ValidateBasicInfoCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }


        [HttpPost("organization/validate-locations")]
        public async Task<IActionResult> ValidateLocations([FromBody] ValidateOrganizationLocationsCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("organization/validate-fields")]
        public async Task<IActionResult> ValidateFields([FromBody] ValidateOrganizationFieldsCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("organization/register-organization")]
        public async Task<IActionResult> RegisterOrganization([FromBody] CompleteOrganizationRegistrationCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        #endregion
    }
}