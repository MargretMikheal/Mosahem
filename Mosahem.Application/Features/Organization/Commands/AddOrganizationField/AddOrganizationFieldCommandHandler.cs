using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities;

namespace Mosahem.Application.Features.Organization.Commands.AddOrganizationField
{
    public class AddOrganizationFieldCommandHandler : IRequestHandler<AddOrganizationFieldCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AddOrganizationFieldCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(AddOrganizationFieldCommand request, CancellationToken cancellationToken)
        {
            var field = await _unitOfWork.Fields.GetByIdAsync(request.FieldId, cancellationToken);
            if (field is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.Validation.NotFound]);

            var organization = await _unitOfWork.Organizations.GetOrganizationWithFieldsAsync(request.OrganizationId, cancellationToken);
            if (organization is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);

            var fields = organization.OrganizationFields;
            if (fields.Any(f => f.FieldId == request.FieldId))
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>>
                    {
                        {"Field" , new(){_localizer[SharedResourcesKeys.State.AlreadyExists]} }
                    });

            var organizationField = request.Adapt<OrganizationField>();
            fields.Add(organizationField);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success<string>(null!, _localizer[SharedResourcesKeys.Success.Added]);
        }
    }
}
