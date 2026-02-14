using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Cities.Queries.GetCitiesByGovernate
{
    public class GetCitiesByGovernateQuery : IRequest<Response<IReadOnlyList<GetCitiesByGovernateResponse>>>
    {
        public Guid GovernateID { get; set; }

        public GetCitiesByGovernateQuery(Guid governateID)
        {
            GovernateID = governateID;
        }
    }
}
