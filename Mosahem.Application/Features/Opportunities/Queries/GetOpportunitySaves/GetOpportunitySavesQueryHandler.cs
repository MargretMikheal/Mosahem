using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Opportunities;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Opportunities.Queries.GetOpportunitySaves
{
    public class GetOpportunitySavesQueryHandler : IRequestHandler<GetOpportunitySavesQuery, Response<List<GetOpportunitySavesResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IFileService _fileService;

        public GetOpportunitySavesQueryHandler(
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

        public async Task<Response<List<GetOpportunitySavesResponse>>> Handle(GetOpportunitySavesQuery request, CancellationToken cancellationToken)
        {
            var saves = await _unitOfWork.Repository<OpportunitySave>()
                .GetTableNoTracking()
                .Where(s => s.OpportunityId == request.OpportunityId)
                .Select(s => new GetOpportunitySavesResponse
                {
                    VolunteerName = s.Volunteer.User.FullName,
                    ProfilePhoto = s.Volunteer.ProfileImgKey
                })
                .ToListAsync(cancellationToken);

            saves.ForEach(s => s.ProfilePhoto = _fileService.GetFileUrl(s.ProfilePhoto, isPrivate: true));

            return _responseHandler.Success(saves, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}
