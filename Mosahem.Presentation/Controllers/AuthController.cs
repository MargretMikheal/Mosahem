using Microsoft.AspNetCore.Mvc;
using mosahem.Application.Features.Authentication.Commands.CompleteOrganizationRegistration;
using mosahem.Application.Features.Authentication.Commands.LoginUser;
using mosahem.Application.Features.Authentication.Commands.RevokeToken;
using mosahem.Application.Features.Authentication.Commands.SendEmailVerificationCode;
using mosahem.Application.Features.Authentication.Commands.ValidateLocations;
using mosahem.Application.Features.Authentication.Commands.ValidateOrganizationBasicInfo;
using mosahem.Application.Features.Authentication.Commands.VerifyEmail;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Authentication.Commands.CompleteVolunteerRegistration;
using Mosahem.Application.Features.Authentication.Commands.RefreshTokens;
using Mosahem.Application.Features.Authentication.Commands.ResetUserPassword.ResetPassword;
using Mosahem.Application.Features.Authentication.Commands.ResetUserPassword.SendRestPasswordOtp;
using Mosahem.Application.Features.Authentication.Commands.ResetUserPassword.VerifyRestPasswordOtp;
using Mosahem.Application.Features.Authentication.Commands.ValidateVolunteerBasicInfo;
using Mosahem.Domain.AppMetaData;

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
        [HttpPost(Router.AuthRouting.ValidateLocations)]
        public async Task<IActionResult> ValidateLocations([FromBody] ValidateLocationsCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }



        #region Organization Registration Flow

        [HttpPost(Router.AuthRouting.OrganizationRegistration.ValidateBasicInfo)]
        public async Task<IActionResult> ValidateBasicInfo([FromBody] ValidateOrganizationBasicInfoCommand command)
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
        #region Volunteer Registration Flow

        [HttpPost(Router.AuthRouting.VolunteerRegistration.ValidateBasicInfo)]
        public async Task<IActionResult> ValidateVolunteerBasicInfo([FromBody] ValidateVolunteerBasicInfoCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost(Router.AuthRouting.VolunteerRegistration.RegisterVolunteer)]
        public async Task<IActionResult> RegisterVolunteer([FromBody] CompleteVolunteerRegistrationCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        #endregion
    }
}