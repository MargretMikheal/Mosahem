using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Files.Commands.Delete;
using Mosahem.Application.Features.Files.Commands.Edit;
using Mosahem.Application.Features.Files.Commands.Edit.EditOrganizationFile;
using Mosahem.Application.Features.Files.Commands.Edit.EditVolunteerFile;
using Mosahem.Application.Features.Files.Commands.Upload;
using Mosahem.Application.Features.Files.Queries.GetFileUrl;
using Mosahem.Domain.AppMetaData;
using Mosahem.Presentation.Filters;
using System.Security.Claims;

namespace Mosahem.Api.Controllers
{
    [ApiController]
    public class FilesController : MosahmControllerBase
    {
        [HttpPost(Router.FileRouting.Upload)]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.FileRouting.GetUrl)]
        public async Task<IActionResult> GetUrl([FromQuery] string key, [FromQuery] bool isPrivate = false)
        {
            var response = await _mediator.Send(new GetFileUrlQuery(key, isPrivate));
            return NewResult(response);
        }

        [HttpDelete(Router.FileRouting.Delete)]
        public async Task<IActionResult> DeleteFile([FromBody] DeleteFileCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.FileRouting.Edit)]
        [Authorize(Roles = $"{nameof(UserRole.Organization)},{nameof(UserRole.Volunteer)}")]
        [ValidateModelId]
        public async Task<IActionResult> EditFile([FromForm] EditUserFileRequest request)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return Unauthorized();

            if (User.IsInRole(nameof(UserRole.Organization)))
                return NewResult(await _mediator.Send(new EditOrganizationFileCommand
                {
                    OrganizationId = userId,
                    OpportunityId = request.OpportunityId,
                    FolderName = request.FolderName,
                    File = request.File
                }));
            else
            {
                return NewResult(await _mediator.Send(new EditVolunteerFileCommand
                {
                    VolunteerId = userId,
                    FolderName = request.FolderName,
                    File = request.File
                }));
            }
        }
    }
}