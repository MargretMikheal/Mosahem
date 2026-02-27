using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Organization.Commands.ValidateOrganization.ApproveOrganization
{
    public class ApproveOrganizationCommandHandler : IRequestHandler<ApproveOrganizationCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ApproveOrganizationCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(ApproveOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(request.OrganizationId, cancellationToken);
            if (organization is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);


            organization.VerificationStatus = VerficationStatus.Approved;
            organization.VerificationComment = request.Comment;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success<string>(null!, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}
