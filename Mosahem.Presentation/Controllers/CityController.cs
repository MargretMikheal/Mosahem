using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mosahem.Domain.Enums;
using mosahem.Presentation.Bases;
using Mosahem.Application.Features.Cities.Commands.AddCity;
using Mosahem.Application.Features.Cities.Queries.GetCitiesByGovernate;
using Mosahem.Domain.AppMetaData;
using Mosahem.Presentation.Filters;

namespace Mosahem.Presentation.Controllers
{
    [ApiController]
    public class CityController : MosahmControllerBase
    {
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost(Router.CityRouting.AddCity)]
        [ValidateModelId]
        public async Task<IActionResult> AddCity([FromBody] AddCityCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.CityRouting.GetCitiesByGovernate)]
        [ValidateModelId]
        public async Task<IActionResult> GetCitiesByGovernate([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetCitiesByGovernateQuery(governateId: id));
            return NewResult(response);
        }
    }
}
