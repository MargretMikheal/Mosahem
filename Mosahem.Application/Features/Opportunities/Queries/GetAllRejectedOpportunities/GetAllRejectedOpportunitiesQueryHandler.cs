using MapsterMapper;
using MediatR;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Opportunities.Queries.GetAllRejectedOpportunities
{
    public class GetAllRejectedOpportunitiesQueryHandler : IRequestHandler<GetAllRejectedOpportunitiesQuery, Response<List<RejectedOpportunityResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public GetAllRejectedOpportunitiesQueryHandler(
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

        public async Task<Response<List<RejectedOpportunityResponse>>> Handle(GetAllRejectedOpportunitiesQuery request, CancellationToken cancellationToken)
        {
            var opportunities = await _unitOfWork.Opportunities.GetRejectedOpportunitiesAsync(cancellationToken);

            var response = _mapper.Map<List<RejectedOpportunityResponse>>(opportunities)
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
