using MediatR;
using Microsoft.AspNetCore.Mvc;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Files.Commands.Delete;
using Mosahem.Application.Features.Files.Commands.Upload;

namespace Mosahem.Api.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : MosahmControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFile([FromBody] DeleteFileCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
    }
}