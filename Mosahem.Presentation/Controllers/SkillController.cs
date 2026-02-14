using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Skills.Commands.AddSkill;
using Mosahem.Application.Features.Skills.Commands.DeleteSkill;
using Mosahem.Application.Features.Skills.Commands.EditSkill;
using Mosahem.Domain.AppMetaData;

namespace Mosahem.Presentation.Controllers
{
    [ApiController]
    public class SkillController : MosahmControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost(Router.SkillRouting.AddSkill)]
        public async Task<IActionResult> AddSkill([FromBody] AddSkillCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete(Router.SkillRouting.DeleteSkill)]
        public async Task<IActionResult> DeleteSkill([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteSkillCommand(id));
            return NewResult(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut(Router.SkillRouting.EditSkill)]
        public async Task<IActionResult> EditSkill([FromBody] EditSkillCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
    }
}
