using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Features.Authentication.Commands.ValidateLocations;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Authentication.Commands.ValidateLocations
{
    public class ValidateLocationsCommandHandler : IRequestHandler<ValidateLocationsCommand, Response<string>>
    {
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ValidateLocationsCommandHandler(ResponseHandler responseHandler, IStringLocalizer<SharedResources> localizer)
        {
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(ValidateLocationsCommand request, CancellationToken cancellationToken)
        {
            return _responseHandler.Success<string>(null, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}
