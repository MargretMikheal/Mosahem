using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;
using static mosahem.Application.Resources.SharedResourcesKeys;

namespace Mosahem.Application.Features.Organization.Queries.GetAllOrganizations
{
    public class GetAllOrganizationsQueryHandler : IRequestHandler<GetAllOrganizationsQuery, Response<List<GetAllOrganizationsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public GetAllOrganizationsQueryHandler(
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

        public async Task<Response<List<GetAllOrganizationsResponse>>> Handle(GetAllOrganizationsQuery request, CancellationToken cancellationToken)
        {
            var organizations = await _unitOfWork.Organizations.GetAllForListingAsync(cancellationToken);
            if (organizations == null)
                return _responseHandler.NotFound<List<GetAllOrganizationsResponse>>(_localizer[User.NotFound]);

            var response = _mapper.Map<List<GetAllOrganizationsResponse>>(organizations);
            response.ForEach(organization =>
                organization.OrganizationLogo = _fileService.GetFileUrl(organization.OrganizationLogo, isPrivate: true));

            return _responseHandler.Success(response, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}
