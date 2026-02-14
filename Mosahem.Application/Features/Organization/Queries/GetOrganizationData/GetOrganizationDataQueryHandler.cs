using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Features.Organization.Queries.GetOrganizationData.Mosahem.Application.Features.Organization.Queries.GetOrganizationData;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Organization.Queries.GetOrganizationData
{
    public class GetOrganizationDataQueryHandler : IRequestHandler<GetOrganizationDataQuery, Response<GetOrganizationDataResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public GetOrganizationDataQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IMapper mapper,
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<Response<GetOrganizationDataResponse>> Handle(GetOrganizationDataQuery request, CancellationToken cancellationToken)
        {
            var organization = await _unitOfWork.Organizations
                .GetOrganizationWithDetailsAsync(request.OrganizationId, cancellationToken);

            if (organization == null)
                return _responseHandler.NotFound<GetOrganizationDataResponse>(_localizer[SharedResourcesKeys.User.NotFound]);

            var response = _mapper.Map<GetOrganizationDataResponse>(organization);
            response.OrganizationLogo = _fileService.GetFileUrl(organization.LogoKey, isPrivate: true);

            return _responseHandler.Success(response, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}
