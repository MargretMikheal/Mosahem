using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Opportunities.Commands.CreateOpportunity;
using Mosahem.Domain.AppMetaData;
using System.Security.Claims;

namespace Mosahem.Presentation.Controllers
{
    [ApiController]
    [Route(Router.OpportunityRouting.Prefix)]
    public class OpportunitiesController : MosahmControllerBase
    {
        [Authorize(Roles = nameof(UserRole.Organization))]
        [HttpPost(Router.OpportunityRouting.Create)]
        public async Task<IActionResult> Create([FromBody] CreateOpportunityCommand command)
        {
            var organizationId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(organizationId) || !Guid.TryParse(organizationId, out var organizationGuid))
                return Unauthorized();

            command.OrganizationId = organizationGuid;

            var response = await _mediator.Send(command);
            return NewResult(response);
        }
    }
}
