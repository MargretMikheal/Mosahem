using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Cities.Queries.GetCitiesByGovernate
{
    public class GetCitiesByGovernateQueryHandler : IRequestHandler<GetCitiesByGovernateQuery, Response<IReadOnlyList<GetCitiesByGovernateResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public GetCitiesByGovernateQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<GetCitiesByGovernateResponse>>> Handle(GetCitiesByGovernateQuery request, CancellationToken cancellationToken)
        {
            //Check if the governate exists
            var governate = await _unitOfWork.Governorates.GetByIdAsync(request.GovernateId, cancellationToken);
            if (governate is null)
                return _responseHandler.NotFound<IReadOnlyList<GetCitiesByGovernateResponse>>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        { nameof(request.GovernateId) , new(){_localizer[SharedResourcesKeys.Validation.NotFound]} }
                    });


            //Get the cities of that governate
            var cities = await _unitOfWork.Cities.GetCitiesByGovernate(request.GovernateId, cancellationToken);
            var citiesResponse = _mapper.Map<IReadOnlyList<GetCitiesByGovernateResponse>>(cities);

            return _responseHandler.Success(citiesResponse);
        }
    }
}
