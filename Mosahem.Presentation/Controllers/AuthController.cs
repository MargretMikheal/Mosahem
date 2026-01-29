using MediatR;
using Microsoft.AspNetCore.Mvc;
using mosahem.Application.Features.Authentication.Commands.CompleteOrganizationRegistration;
using mosahem.Application.Features.Authentication.Commands.ValidateBasicInfo;
using mosahem.Application.Features.Authentication.Commands.ValidateOrganizationLocations; // Check namespace
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Authentication.Commands.ValidateOrganizationFields;

namespace Mosahem.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : MosahmControllerBase
    {
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