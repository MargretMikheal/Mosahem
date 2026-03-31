using Microsoft.AspNetCore.Mvc;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Governates.GetAllGovernates;
using Mosahem.Domain.AppMetaData;

namespace Mosahem.Presentation.Controllers
{
    [ApiController]
    public class GovernateController : MosahmControllerBase
    {
        [HttpGet(Router.GovernateRouting.GetAllGovernates)]
        public async Task<IActionResult> GetAllGovernates()
        {
            var response = await _mediator.Send(new GetAllGovernatesQuery());
            return NewResult(response);
        }
    }
}
