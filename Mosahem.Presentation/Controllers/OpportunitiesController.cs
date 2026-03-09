using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Opportunities.Commands.ApproveOpportunity;
using Mosahem.Application.Features.Opportunities.Commands.CreateOpportunity;
using Mosahem.Application.Features.Opportunities.Commands.RejectOpportunity;
using Mosahem.Application.Features.Opportunities.Commands.ResumeOpportunity;
using Mosahem.Application.Features.Opportunities.Commands.StopOpportunity;
using Mosahem.Application.Features.Opportunities.Queries.GetAllAcceptedOpportunities;
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
        private IUnitOfWork _unitOfWork => HttpContext.RequestServices.GetService<IUnitOfWork>()!;
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

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpGet(Router.OpportunityRouting.GetAccepted)]
        public async Task<IActionResult> GetAccepted()
        {
            var response = await _mediator.Send(new GetAllAcceptedOpportunitiesQuery());
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost(Router.OpportunityRouting.Approve)]
        [ValidateModelId]
        public async Task<IActionResult> ApproveOpportunity([FromRoute] Guid id)
        {
            var command = new ApproveOpportunityCommand { OpportunityId = id };
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost(Router.OpportunityRouting.Reject)]
        [ValidateModelId]
        public async Task<IActionResult> RejectOpportunity([FromRoute] Guid id)
        {
            var command = new RejectOpportunityCommand { OpportunityId = id };
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Organization)}")]
        [HttpPost(Router.OpportunityRouting.Stop)]
        [ValidateModelId]
        public async Task<IActionResult> StopOpportunity([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (!User.IsInRole(nameof(UserRole.Admin)))
            {
                var organizationId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst("sub")?.Value;

                if (string.IsNullOrWhiteSpace(organizationId) || !Guid.TryParse(organizationId, out var organizationGuid))
                    return Unauthorized();

                var isOwnedByOrganization = await _unitOfWork.Opportunities
                    .IsOwnedByOrganizationAsync(id, organizationGuid, cancellationToken);

                if (!isOwnedByOrganization)
                    return Forbid();
            }

            var command = new StopOpportunityCommand { OpportunityId = id };
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Organization)}")]
        [HttpPost(Router.OpportunityRouting.Resume)]
        [ValidateModelId]
        public async Task<IActionResult> ResumeOpportunity([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            if (!User.IsInRole(nameof(UserRole.Admin)))
            {
                var organizationId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst("sub")?.Value;

                if (string.IsNullOrWhiteSpace(organizationId) || !Guid.TryParse(organizationId, out var organizationGuid))
                    return Unauthorized();

                var isOwnedByOrganization = await _unitOfWork.Opportunities
                    .IsOwnedByOrganizationAsync(id, organizationGuid, cancellationToken);

                if (!isOwnedByOrganization)
                    return Forbid();
            }

            var response = await _mediator.Send(new ResumeOpportunityCommand { OpportunityId = id });
            return NewResult(response);
        }
    }
}
