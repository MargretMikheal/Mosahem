using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Location;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunityInfo
{
    public class EditOpportunityInfoCommandHandler : IRequestHandler<EditOpportunityInfoCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EditOpportunityInfoCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(EditOpportunityInfoCommand request, CancellationToken cancellationToken)
        {
            var opportunity = await _unitOfWork.Opportunities.GetByIdAsync(request.OpportunityId, cancellationToken);
            if (opportunity is null || opportunity.OrganizationId != request.OrganizationId)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        { "OpportunityId" , new(){ _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });
            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var AcceptedApplicantsCount = await _unitOfWork.OpportunityApplications.GetAcceptedApplicantsCount(request.OpportunityId, cancellationToken);
                if (request.Vacancies.HasValue && request.Vacancies < AcceptedApplicantsCount)
                {
                    return _responseHandler.BadRequest<string>(
                        _localizer[SharedResourcesKeys.General.OperationFailed],
                        new Dictionary<string, List<string>>
                        {
                        { "Vacancies", new(){_localizer[SharedResourcesKeys.Validation.VacanciesCannotBeLessThanAcceptedApplicants] } }
                        });
                }
                else if (request.Vacancies.HasValue && opportunity.Status.HasFlag(OpportunityStatus.Closed))
                {
                    opportunity.Status &= ~OpportunityStatus.Closed;
                    opportunity.Status |= OpportunityStatus.Open;
                }
                request.Adapt(opportunity);

                if (request.Addresses is not null)
                {
                    if (opportunity.StartDate.AddHours(-48) < DateTime.UtcNow)
                        return _responseHandler.BadRequest<string>(_localizer[SharedResourcesKeys.Validation.LocationsCanBeEditedOnlyBefore48Houres]);

                    if (!request.Addresses.Any() && opportunity.LocationType != OpportunityLocationType.Remote)
                    {
                        return _responseHandler.BadRequest<string>(
                            _localizer[SharedResourcesKeys.General.OperationFailed],
                            new Dictionary<string, List<string>>
                            {
                            { "Addresses", new(){_localizer[SharedResourcesKeys.Validation.AnOnSiteOpportunityMustHaveAtLeastOneLocation] } }
                            });
                    }
                    var opportunityAddresses = await _unitOfWork.Addresses.GetOpportunityAddressAsync(request.OpportunityId, cancellationToken);
                    var opportunityAddressDict = opportunityAddresses.ToDictionary(a => a.Id);


                    var requestAddresses = request.Addresses.Where(add => add.AddressId.HasValue).ToList();
                    var requestAddressesIds = request.Addresses.Where(add => add.AddressId.HasValue).Select(add => add.AddressId!.Value).ToHashSet();

                    var invalidAdressesIdsExist = requestAddressesIds.Any(reqAdd => !opportunityAddressDict.ContainsKey(reqAdd));
                    if (invalidAdressesIdsExist)
                    {
                        return _responseHandler.NotFound<string>(
                            _localizer[SharedResourcesKeys.General.OperationFailed],
                            new Dictionary<string, List<string>>
                            {
                            { "Addresses", new(){_localizer[SharedResourcesKeys.Validation.NotFound] } }
                            });
                    }
                    //Delete
                    var toDelete = opportunityAddresses.Where(add => !requestAddressesIds.Contains(add.Id)).ToList();
                    await _unitOfWork.Addresses.DeleteRangeAsync(toDelete, cancellationToken);
                    //PUt
                    foreach (var reqAddress in requestAddresses)
                    {
                        var existingAddress = opportunityAddressDict[reqAddress.AddressId!.Value];
                        reqAddress.Adapt(existingAddress);
                    }
                    //Add
                    var newAddress = request.Addresses.Where(add => add.AddressId == null).ToList();
                    var toAdd = newAddress.Adapt<List<Address>>();
                    foreach (var address in toAdd)
                        address.OpportunityId = request.OpportunityId;
                    await _unitOfWork.Addresses.AddRangeAsync(toAdd, cancellationToken);
                }
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
