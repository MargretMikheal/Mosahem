using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Opportunities;

namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunityFields
{
    public class EditOpportunityFieldsCommandHandler : IRequestHandler<EditOpportunityFieldsCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EditOpportunityFieldsCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(EditOpportunityFieldsCommand request, CancellationToken cancellationToken)
        {
            /*
             * check for opportunity existance
             * check if the opportunity belongs to that organization
             */
            var opportunity = await _unitOfWork.Opportunities.GetByIdAsync(request.OpportunityId, cancellationToken);
            if (opportunity is null || opportunity.OrganizationId != request.OrganizationId)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        { "OpportunityId" , new(){ _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });

            //Check if the start date is after 48 hours or more
            if (opportunity.StartDate.AddHours(-48) < DateTime.UtcNow)
                return _responseHandler.BadRequest<string>(_localizer[SharedResourcesKeys.Validation.FieldsCanBeEditedOnlyBefore48Houres]);

            //Check if all the fields exist
            var existingFieldsIds = (await _unitOfWork.Fields.GetAllAsync(cancellationToken)).Select(f => f.Id).ToHashSet();

            var invalidFieldIdExist = request.FieldIds.Any(fId => !existingFieldsIds.Contains(fId));
            if (invalidFieldIdExist)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        { "FieldsIds" , new(){ _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });


            //Getting opportunity fields
            var opportunityFields = await _unitOfWork.Repository<OpportunityField>()
                .GetTableAsTracking()
                .Where(of => of.OpportunityId == request.OpportunityId)
                .ToHashSetAsync();

            //Creating the new Fields
            var newOpportunityFields = request.FieldIds
                .Where(fId => opportunityFields.All(of => of.FieldId != fId))
                .Select(fId => new OpportunityField
                {
                    FieldId = fId,
                    OpportunityId = request.OpportunityId,
                }).ToList();

            var toDelete = opportunityFields
                .Where(of => !request.FieldIds.Contains(of.FieldId))
                .ToList();
            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _unitOfWork.Repository<OpportunityField>().AddRangeAsync(newOpportunityFields, cancellationToken);
                await _unitOfWork.Repository<OpportunityField>().DeleteRangeAsync(toDelete, cancellationToken);

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
