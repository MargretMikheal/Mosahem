using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Location;

namespace Mosahem.Application.Features.Addresses.Commands.EditVolunteerAddress
{
    public class EditVolunteerAddressCommandHandler : IRequestHandler<EditVolunteerAddressCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EditVolunteerAddressCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(EditVolunteerAddressCommand request, CancellationToken cancellationToken)
        {
            //check the volunteer exists
            var volunteer = await _unitOfWork.Volunteers.GetByIdAsync(request.VolunteerId, cancellationToken);
            if (volunteer is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);

            //check the address exists
            var address = await _unitOfWork.Addresses.GetVolunteerAddressAsync(request.VolunteerId, cancellationToken);

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

            if (address is null)
            {
                address = request.Adapt<Address>();
                await _unitOfWork.Addresses.AddAsync(address, cancellationToken);
            }
            else
                request.Adapt(address);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success<string>(null!, _localizer[SharedResourcesKeys.General.Updated]);
        }
    }
}
