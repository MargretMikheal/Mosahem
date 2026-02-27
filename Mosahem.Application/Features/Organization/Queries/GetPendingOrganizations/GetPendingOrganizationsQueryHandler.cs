using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Organization.Queries.GetPendingOrganizations
{
    public class GetPendingOrganizationsQueryHandler : IRequestHandler<GetPendingOrganizationsQuery, Response<IReadOnlyList<GetPendingOrganizationsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public GetPendingOrganizationsQueryHandler(
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

        public async Task<Response<IReadOnlyList<GetPendingOrganizationsResponse>>> Handle(GetPendingOrganizationsQuery request, CancellationToken cancellationToken)
        {
            var pendingOrganizations = await _unitOfWork.Organizations.GetPendingOrganizationsPageAsync(request.PageNumber, request.PageSize, cancellationToken);

            var mappedOrganizations = _mapper.Map<IReadOnlyList<GetPendingOrganizationsResponse>>(pendingOrganizations);
            mappedOrganizations = mappedOrganizations
                .Select(org =>
                {
                    org.OrganizationLogo = _fileService.GetFileUrl(org.OrganizationLogo, isPrivate: true);
                    return org;
                })
                .ToList();

            return _responseHandler.Success(mappedOrganizations);
        }
    }
}
