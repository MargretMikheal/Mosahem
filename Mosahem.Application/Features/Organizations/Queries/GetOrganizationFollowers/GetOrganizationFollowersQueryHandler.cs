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

namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationFollowers
{
    public class GetOrganizationFollowersQueryHandler : IRequestHandler<GetOrganizationFollowersQuery, Response<List<GetOrganizationFollowersResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IFileService _fileService;

        public GetOrganizationFollowersQueryHandler(
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

        public async Task<Response<List<GetOrganizationFollowersResponse>>> Handle(GetOrganizationFollowersQuery request, CancellationToken cancellationToken)
        {
            var organizationFollowers = await _unitOfWork.Repository<OrganizationFollower>()
                .GetTableNoTracking()
                .Where(f => f.OrganizationId == request.OrganizationId)
                .Include(f => f.Volunteer)
                .ThenInclude(v => v.User)
                .ToListAsync(cancellationToken);

            var followers = organizationFollowers.Adapt<List<GetOrganizationFollowersResponse>>();

            followers.ForEach(f =>
                f.ProfileImage = _fileService.GetFileUrl(f.ProfileImage, isPrivate: true));

            return _responseHandler.Success(followers, _localizer[General.Success]);
        }
    }
}
