using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Enums;
using Mosahem.Application.Common.Pagination;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Opportunities.Queries.GetAllOpportunitiesByVerificationStatus
{
    public class GetOpportunitiesByVerificationStatusQueryHandler : IRequestHandler<GetAllOpportunitiesByVerificationStatusQuery, Response<PaginatedResponse<GetAllOpportunitiesByVerificationStatusResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _contextAccessor;

        public GetOpportunitiesByVerificationStatusQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IFileService fileService,
            IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _fileService = fileService;
            _contextAccessor = contextAccessor;
        }

        public async Task<Response<PaginatedResponse<GetAllOpportunitiesByVerificationStatusResponse>>> Handle(GetAllOpportunitiesByVerificationStatusQuery request, CancellationToken cancellationToken)
        {
            var verificationStatus = Enum.Parse<VerficationStatus>(request.OpportunityVerificationStatus, ignoreCase: true);

            var (opportunities, totalCount) = await _unitOfWork.Opportunities
                .GetOpportunitiesByVerificationStatusPageAsync(
                    verificationStatus,
                    request.Page,
                    request.PageSize,
                    cancellationToken);

            var mappedOpportunities = opportunities.Adapt<IReadOnlyList<GetAllOpportunitiesByVerificationStatusResponse>>();
            mappedOpportunities = mappedOpportunities
                .Select((opportunity, index) =>
                {
                    opportunity.OrganizationLogoUrl = _fileService.GetFileUrl(opportunities[index].Organization?.LogoKey, isPrivate: true);
                    return opportunity;
                })
                .ToList();

            var response = PaginationHelper.Create(
                mappedOpportunities,
                totalCount,
                request.Page,
                request.PageSize,
                _contextAccessor.HttpContext!.Request);

            return _responseHandler.Success(response);
        }
    }
}
