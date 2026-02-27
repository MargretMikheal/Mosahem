using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Addresses.Commands.Organization.EditOrganizationAddress
{
    public class EditOrganizationAddressCommandHandler : IRequestHandler<EditOrganizationAddressCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EditOrganizationAddressCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(EditOrganizationAddressCommand request, CancellationToken cancellationToken)
        {
            //check the organization exists
            var organization = await _unitOfWork.Organizations.GetByIdAsync(request.OrganizationId, cancellationToken);
            if (organization is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);

            //check the address exists
            var address = await _unitOfWork.Addresses.GetByIdAndOrganizationId(request.AddressId, request.OrganizationId, cancellationToken);
            if (address is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.Validation.NotFound]);

            #region city check
            if (request.GovernateId.HasValue)
            {
                var isCityExist = await _unitOfWork.Cities.IsExistByGovernateAsync((Guid)request.GovernateId, (Guid)request.CityId!, cancellationToken);
                if (!isCityExist)
                    return _responseHandler.NotFound<string>(
                        null!,
                        new Dictionary<string, List<string>>
                        {
                            {nameof(request.CityId) , new(){ _localizer[SharedResourcesKeys.Validation.NotFound] } }
                        });

            }
            else if (request.CityId.HasValue)
            {
                var oldCity = await _unitOfWork.Cities.GetByIdAsync(address.CityId, cancellationToken);
                if (!await _unitOfWork.Cities.IsExistByGovernateAsync(oldCity!.GovernorateId, (Guid)request.CityId, cancellationToken))
                    return _responseHandler.NotFound<string>(
                        null!,
                        new Dictionary<string, List<string>>
                        {
                            {nameof(request.CityId) , new(){ _localizer[SharedResourcesKeys.Validation.NotFound] } }
                        });
            }
            #endregion

            request.Adapt(address);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success<string>(null!, _localizer[SharedResourcesKeys.General.Updated]);
        }
    }
}
