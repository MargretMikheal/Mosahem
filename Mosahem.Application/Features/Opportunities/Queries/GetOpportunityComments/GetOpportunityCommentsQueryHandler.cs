using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Opportunities;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Opportunities.Queries.GetOpportunityComments
{
    public class GetOpportunityCommentsQueryHandler : IRequestHandler<GetOpportunityCommentsQuery, Response<List<GetOpportunityCommentsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IFileService _fileService;

        public GetOpportunityCommentsQueryHandler(
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

        public async Task<Response<List<GetOpportunityCommentsResponse>>> Handle(GetOpportunityCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _unitOfWork.Repository<OpportunityComment>()
                .GetTableNoTracking()
                .Where(c => c.OpportunityId == request.OpportunityId && !c.IsHidden)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new GetOpportunityCommentsResponse
                {
                    VolunteerName = c.Volunteer.User.FullName,
                    ProfilePhoto = c.Volunteer.ProfileImgKey,
                    Comment = c.Text
                })
                .ToListAsync(cancellationToken);

            comments.ForEach(c => c.ProfilePhoto = _fileService.GetFileUrl(c.ProfilePhoto, isPrivate: true));

            return _responseHandler.Success(comments, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}
