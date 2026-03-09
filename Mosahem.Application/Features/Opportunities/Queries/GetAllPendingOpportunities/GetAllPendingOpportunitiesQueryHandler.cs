using MapsterMapper;
using MediatR;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Opportunities.Queries.GetAllPendingOpportunities
{
    public class GetAllPendingOpportunitiesQueryHandler : IRequestHandler<GetAllPendingOpportunitiesQuery, Response<List<PendingOpportunityResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public GetAllPendingOpportunitiesQueryHandler(
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

        public async Task<Response<List<PendingOpportunityResponse>>> Handle(GetAllPendingOpportunitiesQuery request, CancellationToken cancellationToken)
        {
            var opportunities = await _unitOfWork.Opportunities
                .GetPendingOpportunitiesAsync(cancellationToken);

            var response = _mapper.Map<List<PendingOpportunityResponse>>(opportunities)
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
