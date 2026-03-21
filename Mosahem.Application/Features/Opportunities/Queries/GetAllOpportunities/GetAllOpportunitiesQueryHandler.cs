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

namespace Mosahem.Application.Features.Opportunities.Queries.GetAllOpportunities
{
    public class GetAllOpportunitiesQueryHandler : IRequestHandler<GetAllOpportunitiesQuery, Response<PaginatedResponse<GetAllOpportunitiesResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetAllOpportunitiesQueryHandler(
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

        public async Task<Response<PaginatedResponse<GetAllOpportunitiesResponse>>> Handle(GetAllOpportunitiesQuery request, CancellationToken cancellationToken)
        {
            OpportunityStatus? status = null;
            OpportunityWorkType? workType = null;
            OpportunityLocationType? locationType = null;

            if (!string.IsNullOrWhiteSpace(request.OpportunityStatus) &&
                Enum.TryParse<OpportunityStatus>(request.OpportunityStatus, true, out var parsedStatus))
            {
                status = parsedStatus;
            }

            if (!string.IsNullOrWhiteSpace(request.WorkType) &&
                Enum.TryParse<OpportunityWorkType>(request.WorkType, true, out var parsedWorkType))
            {
                workType = parsedWorkType;
            }

            if (!string.IsNullOrWhiteSpace(request.LocationType) &&
                Enum.TryParse<OpportunityLocationType>(request.LocationType, true, out var parsedLocationType))
            {
                locationType = parsedLocationType;
            }

            var (opportunities, totalCount) = await _unitOfWork.Opportunities.GetAllOpportunitiesPageAsync(
                request.Search,
                request.GovernateId,
                status,
                workType,
                locationType,
                request.StartDate,
                request.FieldIds,
                request.RequiredSkillIds,
                request.ProvidedSkillIds,
                request.Page,
                request.PageSize,
                cancellationToken);

            var mappedOpportunities = opportunities.Adapt<IReadOnlyList<GetAllOpportunitiesResponse>>();
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
