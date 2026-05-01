using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Volunteers.Queries.GetVolunteerProfile
{
    public class GetVolunteerProfileQueryHandler : IRequestHandler<GetVolunteerProfileQuery, Response<GetVolunteerProfileResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public GetVolunteerProfileQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IFileService fileService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<Response<GetVolunteerProfileResponse>> Handle(GetVolunteerProfileQuery request, CancellationToken cancellationToken)
        {
            var volunteer = await _unitOfWork.Volunteers.GetVolunteerWithDetailsAsync(request.VolunteerId, cancellationToken);
            if (volunteer == null)
                return _responseHandler.NotFound<GetVolunteerProfileResponse>(_localizer[SharedResourcesKeys.User.NotFound]);

            var response = _mapper.Map<GetVolunteerProfileResponse>(volunteer);

            response.Location = volunteer.Address == null
                ? null
                : $"{volunteer.Address.City.Localize(volunteer.Address.City.NameAr, volunteer.Address.City.NameEn)}, {volunteer.Address.City.Governorate.Localize(volunteer.Address.City.Governorate.NameAr, volunteer.Address.City.Governorate.NameEn)}";

            response.ProfilePhoto = _fileService.GetFileUrl(response.ProfilePhoto, isPrivate: true);
            response.CoverPhoto = _fileService.GetFileUrl(response.CoverPhoto, isPrivate: true);

            foreach (var opportunity in response.CompletedOpportunities)
                opportunity.OpportunityPhotoUrl = _fileService.GetFileUrl(opportunity.OpportunityPhotoUrl, isPrivate: true);

            foreach (var opportunity in response.SavedOpportunities)
                opportunity.OpportunityPhotoUrl = _fileService.GetFileUrl(opportunity.OpportunityPhotoUrl, isPrivate: true);

            return _responseHandler.Success(response);
        }
    }
}
