using Microsoft.AspNetCore.Mvc;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Files.Commands.Delete;
using Mosahem.Application.Features.Files.Commands.Upload;
using Mosahem.Application.Features.Files.Queries.GetFileUrl;
using Mosahem.Domain.AppMetaData;

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
    }
}