using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organization.Commands.DeleteOrganizationField
{
    public class DeleteOrganizationFieldCommandHandler : IRequestHandler<DeleteOrganizationFieldCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public DeleteOrganizationFieldCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(DeleteOrganizationFieldCommand request, CancellationToken cancellationToken)
        {
            var organization = await _unitOfWork.Organizations.GetOrganizationWithFieldsAsync(request.OrganizationId, cancellationToken);
            if (organization is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);

            var fields = organization.OrganizationFields;
            var organizationField = fields.FirstOrDefault(f => f.FieldId == request.FieldId);
            if (organizationField is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.Validation.NotFound]);

            fields.Remove(organizationField);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success<string>(null!, _localizer[SharedResourcesKeys.General.Deleted]);
        }
    }
}
