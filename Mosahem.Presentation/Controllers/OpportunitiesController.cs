using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Opportunities.Commands.CreateOpportunity;
using Mosahem.Application.Features.Opportunities.Queries.GetAllPendingOpportunities;
using Mosahem.Application.Features.Opportunities.Queries.GetOpportunityById;
using Mosahem.Domain.AppMetaData;
using Mosahem.Presentation.Filters;
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
        [AllowAnonymous]
        [HttpGet(Router.OpportunityRouting.GetById)]
        [ValidateModelId]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetOpportunityByIdQuery { OpportunityId = id });
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpGet(Router.OpportunityRouting.GetPending)]
        public async Task<IActionResult> GetPending()
        {
            var response = await _mediator.Send(new GetAllPendingOpportunitiesQuery());
            return NewResult(response);
        }
    }
}
