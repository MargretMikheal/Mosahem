using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Common.Pagination;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Organizations.Queries.GetPendingOrganizations
{
    public class GetPendingOrganizationsQueryHandler : IRequestHandler<GetPendingOrganizationsQuery, Response<PaginatedResponse<GetPendingOrganizationsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetPendingOrganizationsQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IMapper mapper,
            IFileService fileService,
            IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _mapper = mapper;
            _fileService = fileService;
            _contextAccessor = contextAccessor;
        }

        public async Task<Response<PaginatedResponse<GetPendingOrganizationsResponse>>> Handle(GetPendingOrganizationsQuery request, CancellationToken cancellationToken)
        {
            var (pendingOrganizations, totalCount) = await _unitOfWork.Organizations.GetPendingOrganizationsPageAsync(request.Page, request.PageSize, cancellationToken);

            var mappedOrganizations = _mapper.Map<IReadOnlyList<GetPendingOrganizationsResponse>>(pendingOrganizations);
            mappedOrganizations = mappedOrganizations
                .Select(org =>
                {
                    org.OrganizationLogo = _fileService.GetFileUrl(org.OrganizationLogo, isPrivate: true);
                    return org;
                })
                .ToList();
            var paginatedResponse = PaginationHelper.Create(
                mappedOrganizations,
                totalCount,
                request.Page,
                request.PageSize,
                _contextAccessor.HttpContext.Request);

            return _responseHandler.Success(paginatedResponse);
        }
    }
}
