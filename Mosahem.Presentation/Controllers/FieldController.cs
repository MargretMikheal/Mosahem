using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Fields.Commands.AddField;
using Mosahem.Application.Features.Fields.Commands.DeleteField;
using Mosahem.Application.Features.Fields.Commands.EditField;
using Mosahem.Application.Features.Fields.Queries.GetAllFields;
using Mosahem.Domain.AppMetaData;

namespace Mosahem.Presentation.Controllers
{
    [ApiController]
    public class FieldController : MosahmControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost(Router.FieldRouting.AddField)]
        public async Task<IActionResult> AddField([FromBody] AddFieldCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete(Router.FieldRouting.DeleteField)]
        public async Task<IActionResult> DeleteField([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteFieldCommand(id));
            return NewResult(response);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut(Router.FieldRouting.EditField)]
        public async Task<IActionResult> EditField([FromBody] EditFieldCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.FieldRouting.GetAllFields)]
        public async Task<IActionResult> GetAllFields()
        {
            var response = await _mediator.Send(new GetAllFieldsQuery());
            return NewResult(response);
        }
    }
}
