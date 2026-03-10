using MapsterMapper;
using MediatR;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Opportunities.Queries.GetAllAcceptedOpportunities
{
    public class GetAllAcceptedOpportunitiesQueryHandler : IRequestHandler<GetAllAcceptedOpportunitiesQuery, Response<List<AcceptedOpportunityResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public GetAllAcceptedOpportunitiesQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IMapper mapper,
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<Response<List<AcceptedOpportunityResponse>>> Handle(GetAllAcceptedOpportunitiesQuery request, CancellationToken cancellationToken)
        {
            var opportunities = await _unitOfWork.Opportunities.GetAcceptedOpportunitiesAsync(cancellationToken);

            var response = _mapper.Map<List<AcceptedOpportunityResponse>>(opportunities)
                .Select(opportunity =>
                {
                    opportunity.OrganizationLogoUrl = _fileService.GetFileUrl(opportunity.OrganizationLogoUrl, isPrivate: true);
                    return opportunity;
                })
                .ToList();

            return _responseHandler.Success(response);
        }
    }
}
