using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities;

namespace Mosahem.Application.Features.Organizations.Commands.EditOrganizationFields
{
    public class EditOrganizationFieldsCommandHandler : IRequestHandler<EditOrganizationFieldsCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EditOrganizationFieldsCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(EditOrganizationFieldsCommand request, CancellationToken cancellationToken)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(request.OrganizationId, cancellationToken);
            if (organization == null)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        { "OrganizationId" , new(){ _localizer[SharedResourcesKeys.User.NotFound] } }
                    });

            //Check if all the fields exist
            var existingFieldsIds = (await _unitOfWork.Fields.GetAllAsync(cancellationToken)).Select(f => f.Id).ToHashSet();

            var invalidFieldIdExist = request.FieldsIds.Any(fId => !existingFieldsIds.Contains(fId));
            if (invalidFieldIdExist)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        { "FieldsIds" , new(){ _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });

            var organizationFields = await _unitOfWork.Repository<OrganizationField>()
                .GetTableAsTracking()
                .Where(of => of.OrganizationId == request.OrganizationId)
                .ToHashSetAsync();

            //Creating the new Fields
            var newOrganizationFields = request.FieldsIds
                .Where(fId => organizationFields.All(of => of.FieldId != fId))
                .Select(fId => new OrganizationField
                {
                    FieldId = fId,
                    OrganizationId = request.OrganizationId,
                }).ToList();

            var toDelete = organizationFields
                .Where(of => !request.FieldsIds.Contains(of.FieldId))
                .ToList();

            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _unitOfWork.Repository<OrganizationField>().AddRangeAsync(newOrganizationFields, cancellationToken);
                await _unitOfWork.Repository<OrganizationField>().DeleteRangeAsync(toDelete, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return _responseHandler.Success<string>(null!, _localizer[SharedResourcesKeys.General.Updated]);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>>
                    {
                         {"Exception" , new(){ ex.Message} }
                    });
            }
        }
    }
}
