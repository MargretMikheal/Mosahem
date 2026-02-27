using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Cities.Queries.GetCitiesByGovernate
{
    public class GetCitiesByGovernateQuery : IRequest<Response<IReadOnlyList<GetCitiesByGovernateResponse>>>
    {
        public Guid GovernateId { get; set; }

        public GetCitiesByGovernateQuery(Guid governateId)
        {
            GovernateId = governateId;
        }
    }
}
