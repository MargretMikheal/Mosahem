using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Organizations.Commands.EditOrganizationVerificationComment
{
    public class EditOrganizationVerificationCommentCommandHandler : IRequestHandler<EditOrganizationVerificationCommentCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EditOrganizationVerificationCommentCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(EditOrganizationVerificationCommentCommand request, CancellationToken cancellationToken)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(request.OrganizationId, cancellationToken);

            if (organization is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);

            if (organization.VerificationStatus != VerficationStatus.Rejected)
                return _responseHandler.BadRequest<string>(_localizer[SharedResourcesKeys.Validation.BadRequest]);

            organization.VerificationComment = request.VerificationComment;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success(string.Empty, _localizer[SharedResourcesKeys.General.Updated]);
        }
    }
}
