using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Fields.Commands.AddField;
using Mosahem.Application.Features.Fields.Commands.DeleteField;
using Mosahem.Application.Features.Fields.Commands.EditField;
using Mosahem.Application.Features.Fields.Queries.GetAllFields;
using Mosahem.Application.Features.Fields.Queries.GetOrganizationFields;
using Mosahem.Domain.AppMetaData;
using Mosahem.Presentation.Filters;

namespace Mosahem.Presentation.Controllers
{
    [ApiController]
    public class FieldController : MosahmControllerBase
    {
        #region Admin
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost(Router.FieldRouting.AddField)]
        public async Task<IActionResult> AddField([FromBody] AddFieldCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpDelete(Router.FieldRouting.DeleteField)]
        [ValidateModelId]
        public async Task<IActionResult> DeleteField([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteFieldCommand(id));
            return NewResult(response);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPut(Router.FieldRouting.EditField)]
        [ValidateModelId]
        public async Task<IActionResult> EditField(
            [FromRoute] Guid id,
            [FromBody] EditFieldRequest request)
        {
            var response = await _mediator.Send(new EditFieldCommand
            {
                Id = id,
                NameAr = request.NameAr,
                NameEn = request.NameEn
            });
            return NewResult(response);
        }
        #endregion
        [HttpGet(Router.FieldRouting.GetAllFields)]
        public async Task<IActionResult> GetAllFields()
        {
            var response = await _mediator.Send(new GetAllFieldsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.OrganizationRouting.Fields)]
        public async Task<IActionResult> GetOrganizationFields(Guid id)
        {
            var response = await _mediator.Send(new GetOrganizationFieldsQuery { OrganizationId = id });
            return NewResult(response);
        }
    }
}
