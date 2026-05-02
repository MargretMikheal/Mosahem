using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Common.Pagination;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Volunteers.Queries.GetUnratedVolunteers
{
    public class GetUnratedVolunteersQueryHandler : IRequestHandler<GetUnratedVolunteersQuery, Response<PaginatedResponse<GetUnratedVolunteersResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _contextAccessor;
        public GetUnratedVolunteersQueryHandler(
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

        public async Task<Response<PaginatedResponse<GetUnratedVolunteersResponse>>> Handle(GetUnratedVolunteersQuery request, CancellationToken cancellationToken)
        {
            // Get Organization
            var organization = await _unitOfWork.Organizations.GetByIdAsync(request.OrganizationId, cancellationToken);

            if (organization is null)
                return _responseHandler.NotFound<PaginatedResponse<GetUnratedVolunteersResponse>>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        {"OrganizationId" , new List<string> {_localizer[SharedResourcesKeys.User.NotFound]} }
                    });

            var (applications, totalCount) = await _unitOfWork.OpportunityApplications.GetApplicationsWithUnratedVolunteersByOrganizationIdAsync(
                request.OrganizationId,
                request.Page,
                request.PageSize,
                cancellationToken);

            var volunteerResult = applications.Adapt<IReadOnlyList<GetUnratedVolunteersResponse>>();
            volunteerResult = volunteerResult.Select((volunteer, index) =>
            {
                volunteer.ProfileImage = _fileService.GetFileUrl(applications[index].Volunteer.ProfileImgKey, isPrivate: true);
                return volunteer;
            }).ToList();

            var response = PaginationHelper.Create(
                volunteerResult,
                totalCount,
                request.Page,
                request.PageSize,
                _contextAccessor.HttpContext.Request);

            return _responseHandler.Success(response);
        }
    }
}
