using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Location;

namespace Mosahem.Application.Features.Addresses.Commands.Organization.AddOrganizationAddress
{
    public class AddOrganizationAddressCommandHandler : IRequestHandler<AddOrganizationAddressCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AddOrganizationAddressCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(AddOrganizationAddressCommand request, CancellationToken cancellationToken)
        {
            var cityExist = await _unitOfWork.Cities.IsExistByGovernateAsync(request.GovernateId, request.CityID, cancellationToken);
            if (!cityExist)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                       { nameof(request.CityID) ,new (){ _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });

            var address = request.Adapt<Address>();
            await _unitOfWork.Addresses.AddAsync(address, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Created<string>(_localizer[SharedResourcesKeys.Success.Added]);
        }
    }
}
