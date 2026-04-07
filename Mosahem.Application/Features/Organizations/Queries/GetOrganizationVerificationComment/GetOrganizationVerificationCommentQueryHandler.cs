using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationVerificationComment
{
    internal class GetOrganizationVerificationCommentQueryHandler : IRequestHandler<GetOrganizationVerificationCommentQuery, Response<GetOrganizationVerificationCommentResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public GetOrganizationVerificationCommentQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<GetOrganizationVerificationCommentResponse>> Handle(GetOrganizationVerificationCommentQuery request, CancellationToken cancellationToken)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(request.OrganizationId, cancellationToken);

            if (organization is null)
                return _responseHandler.NotFound<GetOrganizationVerificationCommentResponse>(_localizer[SharedResourcesKeys.User.NotFound]);

            var response = new GetOrganizationVerificationCommentResponse
            {
                IsRejected = organization.VerificationStatus == VerficationStatus.Rejected,
                VerificationComment = organization.VerificationStatus == VerficationStatus.Rejected
                    ? organization.VerificationComment
                    : null
            };

            return _responseHandler.Success(response, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}
