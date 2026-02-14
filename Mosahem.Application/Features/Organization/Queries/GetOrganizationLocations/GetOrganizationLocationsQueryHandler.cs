using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organization.Queries.GetOrganizationLocations
{
    public class GetOrganizationLocationsQueryHandler : IRequestHandler<GetOrganizationLocationsQuery, Response<List<GetOrganizationLocationsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public GetOrganizationLocationsQueryHandler(
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

        public async Task<Response<List<GetOrganizationLocationsResponse>>> Handle(GetOrganizationLocationsQuery request, CancellationToken cancellationToken)
        {
            var organizationAddresses = await _unitOfWork.Addresses
                .GetOrganizationAddressesAsync(request.OrganizationId, cancellationToken);

            if (!organizationAddresses.Any())
                return _responseHandler.NotFound<List<GetOrganizationLocationsResponse>>(_localizer[SharedResourcesKeys.Validation.NotFound]);

            var response = _mapper.Map<List<GetOrganizationLocationsResponse>>(organizationAddresses);

            return _responseHandler.Success(response, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}
