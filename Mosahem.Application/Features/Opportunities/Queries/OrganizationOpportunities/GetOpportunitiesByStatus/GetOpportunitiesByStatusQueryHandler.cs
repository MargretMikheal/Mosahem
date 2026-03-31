using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;
using Mosahem.Application.Common.Pagination;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Opportunities.Queries.OrganizationOpportunities.GetOpportunitiesByStatus
{
    public class GetOpportunitiesByStatusQueryHandler : IRequestHandler<GetOpportunitiesByStatusQuery, Response<PaginatedResponse<GetOpportunitiesByStatusResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetOpportunitiesByStatusQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IFileService fileService,
            IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _fileService = fileService;
            _contextAccessor = contextAccessor;
        }

        public async Task<Response<PaginatedResponse<GetOpportunitiesByStatusResponse>>> Handle(GetOpportunitiesByStatusQuery request, CancellationToken cancellationToken)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(request.OrganizationId, cancellationToken);
            if (organization is null)
                return _responseHandler
                    .NotFound<PaginatedResponse<GetOpportunitiesByStatusResponse>>(
                    _localizer[SharedResourcesKeys.User.NotFound]);

            var status = Enum.Parse<OpportunityStatus>(request.OpportunityStatus, ignoreCase: true);
            var (opportunities, totalCount) = await _unitOfWork.Opportunities
                .GetOrganizationOpportunitiesByStatusPageAsync(
                request.OrganizationId,
                status,
                request.Page,
                request.PageSize,
                cancellationToken);

            var mappedOpportunities = opportunities.Adapt<IReadOnlyList<GetOpportunitiesByStatusResponse>>();
            mappedOpportunities = mappedOpportunities.Select((mo, index) =>
            {
                mo.OpportunityPhotoUrl = _fileService.GetFileUrl(opportunities[index].PhotoKey, isPrivate: true);
                mo.Organization.OrganizationLogoUrl = _fileService.GetFileUrl(opportunities[index].Organization?.LogoKey, isPrivate: true);
                return mo;
            }).ToList();
            var response = PaginationHelper.Create(
                mappedOpportunities,
                totalCount,
                request.Page,
                request.PageSize,
                _contextAccessor.HttpContext.Request);

            return _responseHandler.Success(response);
        }
    }
}
