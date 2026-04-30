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

namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationVolunteersByVerificationStatus
{
    public class GetOrganizationVolunteersByVerificationStatusQueryHandler : IRequestHandler<GetOrganizationVolunteersByVerificationStatusQuery, Response<PaginatedResponse<GetOrganizationVolunteersByVerificationStatusResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;

        public GetOrganizationVolunteersByVerificationStatusQueryHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler, IStringLocalizer<SharedResources> localizer, IHttpContextAccessor httpContextAccessor, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }

        public async Task<Response<PaginatedResponse<GetOrganizationVolunteersByVerificationStatusResponse>>> Handle(GetOrganizationVolunteersByVerificationStatusQuery request, CancellationToken cancellationToken)
        {
            var organizationExists = await _unitOfWork.Organizations.ExistsAsync(request.OrganizationId, cancellationToken);
            if (!organizationExists)
                return _responseHandler.NotFound<PaginatedResponse<GetOrganizationVolunteersByVerificationStatusResponse>>(_localizer[SharedResourcesKeys.User.NotFound]);

            var applicantStatus = Enum.Parse<ApplicantStatus>(request.VerificationStatus, ignoreCase: true);
            var (volunteers, totalCount) = await _unitOfWork.OpportunityApplications.GetOrganizationVolunteersByStatusPageAsync(request.OrganizationId, applicantStatus, request.Page, request.PageSize, cancellationToken);

            var mappedVolunteers = volunteers.Adapt<IReadOnlyList<GetOrganizationVolunteersByVerificationStatusResponse>>();
            mappedVolunteers = mappedVolunteers.Select(v =>
            {
                v.ProfileImgUrl = _fileService.GetFileUrl(v.ProfileImgUrl, true);
                return v;
            }).ToList();

            var paginatedResponse = PaginationHelper.Create(mappedVolunteers, totalCount, request.Page, request.PageSize, _httpContextAccessor.HttpContext.Request);
            return _responseHandler.Success(paginatedResponse);
        }
    }
}
