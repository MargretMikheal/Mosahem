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

namespace Mosahem.Application.Features.Opportunities.Queries.GetApplicantsByStatus
{
    public class GetApplicantsByStatusQueryHandler
        : IRequestHandler<GetApplicantsByStatusQuery, Response<PaginatedResponse<GetApplicantsByStatusResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;

        public GetApplicantsByStatusQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IHttpContextAccessor httpContextAccessor,
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }

        async Task<Response<PaginatedResponse<GetApplicantsByStatusResponse>>> IRequestHandler<GetApplicantsByStatusQuery, Response<PaginatedResponse<GetApplicantsByStatusResponse>>>.Handle(GetApplicantsByStatusQuery request, CancellationToken cancellationToken)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(request.OrganizationId, cancellationToken);

            if (organization == null)
                return _responseHandler.NotFound<PaginatedResponse<GetApplicantsByStatusResponse>>(
                    _localizer[SharedResourcesKeys.User.NotFound]);

            var owendByOrganization = await _unitOfWork.Opportunities.IsOwnedByOrganizationAsync(
                request.OpportunityId,
                request.OrganizationId,
                cancellationToken);

            if (!owendByOrganization)
                return _responseHandler.NotFound<PaginatedResponse<GetApplicantsByStatusResponse>>(
                    _localizer[SharedResourcesKeys.Validation.NotFound]);

            // جيب الـ applicants بناءً على الـ Status
            var applicantStatus = Enum.Parse<ApplicantStatus>(request.Status, ignoreCase: true);
            var (applicants, totalcount) = await _unitOfWork.OpportunityApplications.GetApplicantsByStatusPageAsync(request.OpportunityId,
                applicantStatus,
                request.Page,
                request.PageSize,
                cancellationToken);

            var mappedApplicants = applicants.Adapt<IReadOnlyList<GetApplicantsByStatusResponse>>();
            mappedApplicants = mappedApplicants.Select(applicant =>
            {
                applicant.ProfileImgUrl = _fileService.GetFileUrl(applicant.ProfileImgUrl, true);
                return applicant;
            }).ToList();

            var paginatedResponse = PaginationHelper.Create(
                mappedApplicants,
                totalcount,
                request.Page,
                request.PageSize,
                _httpContextAccessor.HttpContext.Request);

            return _responseHandler.Success(paginatedResponse);
        }
    }
}
