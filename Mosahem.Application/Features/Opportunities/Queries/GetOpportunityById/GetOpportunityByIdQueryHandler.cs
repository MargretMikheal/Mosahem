using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Opportunities.Queries.GetOpportunityById
{
    public class GetOpportunityByIdQueryHandler : IRequestHandler<GetOpportunityByIdQuery, Response<OpportunityDetailsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public GetOpportunityByIdQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IMapper mapper,
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<Response<OpportunityDetailsResponse>> Handle(GetOpportunityByIdQuery request, CancellationToken cancellationToken)
        {
            var opportunity = await _unitOfWork.Opportunities
                .GetOpportunityWithDetailsAsync(request.OpportunityId, cancellationToken);

            if (opportunity is null)
            {
                return _responseHandler.NotFound<OpportunityDetailsResponse>(_localizer[SharedResourcesKeys.Validation.NotFound]);
            }

            var response = _mapper.Map<OpportunityDetailsResponse>(opportunity);
            response.OpportunityPhotoUrl = _fileService.GetFileUrl(opportunity.PhotoKey, isPrivate: true);
            response.Organization.OrganizationLogoUrl = _fileService.GetFileUrl(opportunity.Organization?.LogoKey, isPrivate: true);

            return _responseHandler.Success(response);
        }
    }
}
