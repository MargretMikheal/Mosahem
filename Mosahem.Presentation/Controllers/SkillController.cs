using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Skills.Commands.AddSkill;
using Mosahem.Application.Features.Skills.Commands.DeleteSkill;
using Mosahem.Application.Features.Skills.Commands.EditSkill;
using Mosahem.Application.Features.Skills.Queries.GetAllSkills;
using Mosahem.Application.Features.Skills.Queries.GetAllSkillsGrouped;
using Mosahem.Domain.AppMetaData;
using Mosahem.Presentation.Filters;

namespace Mosahem.Presentation.Controllers
{
    [ApiController]
    public class SkillController : MosahmControllerBase
    {
        #region Admin
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost(Router.SkillRouting.AddSkill)]
        public async Task<IActionResult> AddSkill([FromBody] AddSkillCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpDelete(Router.SkillRouting.DeleteSkill)]
        [ValidateModelId]
        public async Task<IActionResult> DeleteSkill([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteSkillCommand(id));
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPut(Router.SkillRouting.EditSkill)]
        [ValidateModelId]
        public async Task<IActionResult> EditSkill(
            Guid id,
            [FromBody] EditSkillRequest request)
        {
            var response = await _mediator.Send(new EditSkillCommand
            {
                Id = id,
                NameAr = request.NameAr,
                NameEn = request.NameEn,
                FieldId = request.FieldId
            });
            return NewResult(response);
        }
        #endregion
        [HttpGet(Router.SkillRouting.GetAllSkills)]
        public async Task<IActionResult> GetAllSkills()
        {
            var response = await _mediator.Send(new GetAllSkillsQuery());
            return NewResult(response);
        }
        [HttpGet(Router.SkillRouting.GetAllSkillsGrouped)]
        public async Task<IActionResult> GetAllSkillsGrouped([FromBody] GetAllSkillsGroupedQuery? query)
        {
            var response = await _mediator.Send(query ?? new GetAllSkillsGroupedQuery());
            return NewResult(response);
        }
    }
}
