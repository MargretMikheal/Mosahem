using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Cities.Commands.AddCity
{
    public class AddCityCommand : IRequest<Response<string>>
    {
        public Guid GovernorateId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }
}
