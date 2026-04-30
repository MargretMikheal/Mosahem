using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Opportunities;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Opportunities.Queries.GetOpportunityLikes
{
    public class GetOpportunityLikesQueryHandler : IRequestHandler<GetOpportunityLikesQuery, Response<List<GetOpportunityLikesResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IFileService _fileService;

        public GetOpportunityLikesQueryHandler(
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

        public async Task<Response<List<GetOpportunityLikesResponse>>> Handle(GetOpportunityLikesQuery request, CancellationToken cancellationToken)
        {
            var likes = await _unitOfWork.Repository<OpportunityLike>()
                .GetTableNoTracking()
                .Where(l => l.OpportunityId == request.OpportunityId)
                .Select(l => new GetOpportunityLikesResponse
                {
                    VolunteerName = l.Volunteer.User.FullName,
                    ProfilePhoto = l.Volunteer.ProfileImgKey
                })
                .ToListAsync(cancellationToken);

            likes.ForEach(l => l.ProfilePhoto = _fileService.GetFileUrl(l.ProfilePhoto, isPrivate: true));

            return _responseHandler.Success(likes, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}
