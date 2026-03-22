using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Opportunities.Commands.ApproveOpportunity;
using Mosahem.Application.Features.Opportunities.Commands.CreateOpportunity;
using Mosahem.Application.Features.Opportunities.Commands.EditOpportunityFields;
using Mosahem.Application.Features.Opportunities.Commands.EditOpportunityInfo;
using Mosahem.Application.Features.Opportunities.Commands.EditOpportunityQuestions;
using Mosahem.Application.Features.Opportunities.Commands.EditOpportunitySkills;
using Mosahem.Application.Features.Opportunities.Commands.RejectOpportunity;
using Mosahem.Application.Features.Opportunities.Commands.ResumeOpportunity;
using Mosahem.Application.Features.Opportunities.Commands.StopOpportunity;
using Mosahem.Application.Features.Opportunities.Queries.GetAllOpportunities;
using Mosahem.Application.Features.Opportunities.Queries.GetAllOpportunitiesByVerificationStatus;
using Mosahem.Application.Features.Opportunities.Queries.GetOpportunityById;
using Mosahem.Application.Features.Opportunities.Queries.OrganizationOpportunities.GetOpportunitiesByStatus;
using Mosahem.Application.Features.Opportunities.Queries.OrganizationOpportunities.GetOpportunitiesByVerificationStatus;
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
        [HttpGet(Router.OpportunityRouting.GetByVerificationStatus)]
        public async Task<IActionResult> GetByVerificationStatus([FromQuery] GetAllOpportunitiesByVerificationStatusQuery query)
        {
            var response = await _mediator.Send(query);
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
        [HttpGet(Router.OpportunityRouting.GetAll)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] GetAllOpportunitiesQuery query)
        {
            var response = await _mediator.Send(query);
            return NewResult(response);
        }
        #region Get Organization Opportunities
        [HttpGet($"/{Router.OrganizationRouting.GetOpportunitiesByVerificationStatus}")]
        [AllowAnonymous]
        [ValidateModelId]
        public async Task<IActionResult> GetByVerificationStatusForOrganization(
            [FromRoute] Guid organizationId,
            [FromQuery] GetOpportunitiesByVerificationStatusRequest request)
        {
            var response = await _mediator.Send(new GetOpportunitiesByVerificationStatusQuery
            {
                OrganizationId = organizationId,
                OpportunityVerificationStatus = request.OpportunityVerificationStatus,
                Page = request.Page,
                PageSize = request.PageSize,
            });
            return NewResult(response);
        }

        [HttpGet($"/{Router.OrganizationRouting.GetOpportunitiesByStatus}")]
        [AllowAnonymous]
        [ValidateModelId]
        public async Task<IActionResult> GetByStatusForOrganization(
            [FromRoute] Guid organizationId,
            [FromQuery] GetOpportunitiesByStatusRequest request)
        {
            var response = await _mediator.Send(new GetOpportunitiesByStatusQuery
            {
                OrganizationId = organizationId,
                OpportunityStatus = request.OpportunityStatus,
                Page = request.Page,
                PageSize = request.PageSize,
            });
            return NewResult(response);
        }
        #endregion
        #region Put Opportuninty
        [HttpPut(Router.OpportunityRouting.EditFields)]
        [Authorize(Roles = nameof(UserRole.Organization))]
        [ValidateModelId]
        public async Task<IActionResult> EditFields([FromRoute] Guid id, [FromBody] EditOpportunityFieldsRequest request)
        {
            var organizationId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(organizationId) || !Guid.TryParse(organizationId, out var organizationGuid))
                return Unauthorized();

            var response = await _mediator.Send(new EditOpportunityFieldsCommand(
                id,
                request.FieldsIds,
                organizationGuid));
            return NewResult(response);
        }
        [HttpPut(Router.OpportunityRouting.EditSkills)]
        [Authorize(Roles = nameof(UserRole.Organization))]
        [ValidateModelId]
        public async Task<IActionResult> EditSkills([FromRoute] Guid id, [FromBody] EditOpportunitySkillsRequest request)
        {
            var organizationId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(organizationId) || !Guid.TryParse(organizationId, out var organizationGuid))
                return Unauthorized();

            var response = await _mediator.Send(new EditOpportunitySkillsCommand(
                id,
                organizationGuid,
                request.SkillType,
                request.SkillsIds
                ));
            return NewResult(response);
        }
        [HttpPut(Router.OpportunityRouting.EditQuestions)]
        [Authorize(Roles = nameof(UserRole.Organization))]
        [ValidateModelId]
        public async Task<IActionResult> EditQuestions([FromRoute] Guid id, [FromBody] EditOpportunityQuestionsRequest request)
        {
            var organizationId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(organizationId) || !Guid.TryParse(organizationId, out var organizationGuid))
                return Unauthorized();

            var response = await _mediator.Send(new EditOpportunityQuestionsCommand(
                request.OpportunityId,
                organizationGuid,
                request.Questions
                ));
            return NewResult(response);
        }

        [HttpPut(Router.OpportunityRouting.EditInfo)]
        [Authorize(Roles = nameof(UserRole.Organization))]
        [ValidateModelId]
        public async Task<IActionResult> EditInfo([FromRoute] Guid id, [FromBody] EditOpportunityInfoRequest request)
        {
            var organizationId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(organizationId) || !Guid.TryParse(organizationId, out var organizationGuid))
                return Unauthorized();

            var response = await _mediator.Send(new EditOpportunityInfoCommand
            {
                Description = request.Description,
                OpportunityId = id,
                OrganizationId = organizationGuid,
                Title = request.Title,
                Vacancies = request.Vacancies,
                Addresses = request.Addresses
            });
            return NewResult(response);
        }
        #endregion
    }
}
