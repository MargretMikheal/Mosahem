using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Volunteers.Commands.DeleteVolunteerAddress
{
    public class DeleteVolunteerAddressCommandHandler : IRequestHandler<DeleteVolunteerAddressCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public DeleteVolunteerAddressCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(DeleteVolunteerAddressCommand request, CancellationToken cancellationToken)
        {
            var volunteer = await _unitOfWork.Volunteers.GetByIdAsync(request.VolunteerId, cancellationToken);
            if (volunteer == null)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        { "VolunteerId", new() { _localizer[SharedResourcesKeys.User.NotFound] } }
                    });

            var address = await _unitOfWork.Addresses.GetVolunteerAddressAsync(request.VolunteerId, cancellationToken);
            if (address == null)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        { "Address", new() { _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });

            await _unitOfWork.Addresses.DeleteAsync(address.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Deleted<string>();
        }
    }
}
