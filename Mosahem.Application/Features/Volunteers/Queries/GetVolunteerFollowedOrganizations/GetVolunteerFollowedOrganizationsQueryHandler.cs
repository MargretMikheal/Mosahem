using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities;
using Mosahem.Application.Interfaces;
using static mosahem.Application.Resources.SharedResourcesKeys;

namespace Mosahem.Application.Features.Volunteers.Queries.GetVolunteerFollowedOrganizations
{
    public class GetVolunteerFollowedOrganizationsQueryHandler : IRequestHandler<GetVolunteerFollowedOrganizationsQuery, Response<List<GetVolunteerFollowedOrganizationsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IFileService _fileService;

        public GetVolunteerFollowedOrganizationsQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _fileService = fileService;
        }

        public async Task<Response<List<GetVolunteerFollowedOrganizationsResponse>>> Handle(GetVolunteerFollowedOrganizationsQuery request, CancellationToken cancellationToken)
        {
            var followedOrganizations = await _unitOfWork.Repository<OrganizationFollower>()
                .GetTableNoTracking()
                .Where(f => f.VolunteerId == request.VolunteerId)
                .Include(f => f.Organization)
                .ThenInclude(o => o.User)
                .ToListAsync(cancellationToken);

            var organizations = followedOrganizations.Adapt<List<GetVolunteerFollowedOrganizationsResponse>>();

            organizations.ForEach(o =>
                o.OrganizationLogo = _fileService.GetFileUrl(o.OrganizationLogo, isPrivate: true));

            return _responseHandler.Success(organizations, _localizer[General.Success]);
        }
    }
}
