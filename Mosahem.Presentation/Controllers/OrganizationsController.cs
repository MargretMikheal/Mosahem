using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Organization.Commands.AddOrganizationField;
using Mosahem.Application.Features.Organization.Commands.DeleteOrganizationField;
using Mosahem.Application.Features.Organization.Commands.EditOrganizationInfo;
using Mosahem.Application.Features.Organization.Commands.ValidateOrganization.ApproveOrganization;
using Mosahem.Application.Features.Organization.Commands.ValidateOrganization.RejectOrganization;
using Mosahem.Application.Features.Organization.Queries.GetAllOrganizations;
using Mosahem.Application.Features.Organization.Queries.GetOrganizationData;
using Mosahem.Application.Features.Organization.Queries.GetOrganizationLicense;
using Mosahem.Application.Features.Organization.Queries.GetPendingOrganizations;
using Mosahem.Domain.AppMetaData;
using Mosahem.Presentation.Filters;
using System.Security.Claims;

namespace Mosahem.Presentation.Controllers
{
    [Route(Router.OrganizationRouting.Prefix)]
    [ApiController]
    public class OrganizationsController : MosahmControllerBase
    {
        #region Public 
        [HttpGet(Router.OrganizationRouting.AllOrganizations)]
        public async Task<IActionResult> GetAllOrganizations()
        {
            var response = await _mediator.Send(new GetAllOrganizationsQuery());
            return NewResult(response);
        }

        #endregion
        #region Organization Authorized Endpoints
        [Authorize(Roles = nameof(UserRole.Organization))]
        [HttpGet(Router.OrganizationRouting.OrganizationData)]
        public async Task<IActionResult> GetOrganizationData()
        {
            var organizationIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(organizationIdString) || !Guid.TryParse(organizationIdString, out Guid organizationId))
                return Unauthorized();

            var response = await _mediator.Send(new GetOrganizationDataQuery { OrganizationId = organizationId });
            return NewResult(response);
        }

        [Authorize(Roles = $"{nameof(UserRole.Organization)},{nameof(UserRole.Admin)}")]
        [HttpGet(Router.OrganizationRouting.GetOrganizationLisence)]
        [ValidateModelId]
        public async Task<IActionResult> GetLicenseUrl([FromRoute] Guid id)
        {
            if (User.IsInRole(nameof(UserRole.Organization)))
            {
                var orgIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst("sub")?.Value;
                if (string.IsNullOrEmpty(orgIdString) || !Guid.TryParse(orgIdString, out Guid organizationId))
                    return Unauthorized();

                if (!organizationId.Equals(id))
                    return Forbid();

            }
            var response = await _mediator.Send(new GetOrganizationLicenseQuery(organizationId: id));
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Organization))]
        [HttpPut(Router.OrganizationRouting.EditOrganizationInfo)]
        [ValidateModelId]
        public async Task<IActionResult> EditOrganizationInfo([FromBody] EditOrganizationInfoCommandRequest request)
        {
            var orgIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(orgIdString) || !Guid.TryParse(orgIdString, out Guid organizationId))
                return Unauthorized();

            var response = await _mediator.Send(new EditOrganizationInfoCommand
            {
                OrganizationId = organizationId,
                OrganizationName = request.OrganizationName,
                OrganizationDescription = request.OrganizationDescription,
                OrganizationPhoneNumber = request.OrganizationPhoneNumber,
            });
            return NewResult(response);
        }
        [Authorize(Roles = nameof(UserRole.Organization))]
        [HttpPut(Router.OrganizationRouting.AddOrganizationAddress)]
        [ValidateModelId]
        public async Task<IActionResult> AddOrganizationField([FromRoute] Guid fieldId)
        {
            var orgIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(orgIdString) || !Guid.TryParse(orgIdString, out Guid organizationId))
                return Unauthorized();

            var response = await _mediator.Send(new AddOrganizationFieldCommand
            {
                OrganizationId = organizationId,
                FieldId = fieldId
            });
            return NewResult(response);
        }
        [Authorize(Roles = nameof(UserRole.Organization))]
        [HttpPut(Router.OrganizationRouting.DeleteOrganizationAddress)]
        [ValidateModelId]
        public async Task<IActionResult> DeleteOrganizationField([FromRoute] Guid fieldId)
        {
            var orgIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(orgIdString) || !Guid.TryParse(orgIdString, out Guid organizationId))
                return Unauthorized();

            var response = await _mediator.Send(new DeleteOrganizationFieldCommand
            {
                OrganizationId = organizationId,
                FieldId = fieldId
            });
            return NewResult(response);
        }
        #endregion
        #region Admin
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpGet(Router.OrganizationRouting.GetPendingOrganizations)]
        public async Task<IActionResult> GetPendingOrganizations([FromQuery] GetPendingOrganizationsQuery query)
        {
            var response = await _mediator.Send(query);
            return NewResult(response);

        }
        #region Organization Validation
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost(Router.OrganizationRouting.ApproveOrganization)]
        [ValidateModelId]
        public async Task<IActionResult> ApproveOrganization([FromBody] ApproveOrganizationCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost(Router.OrganizationRouting.RejectOrganization)]
        [ValidateModelId]
        public async Task<IActionResult> RejectOrganization([FromBody] RejectOrganizationCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        #endregion

        #endregion
    }
}
