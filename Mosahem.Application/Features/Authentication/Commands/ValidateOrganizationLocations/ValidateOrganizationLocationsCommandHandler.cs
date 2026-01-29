using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Features.Authentication.Commands.ValidateOrganizationLocations;
using mosahem.Application.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mosahem.Application.Features.Authentication.Commands.ValidateOrganizationLocations
{
    public class ValidateOrganizationLocationsCommandHandler : IRequestHandler<ValidateOrganizationLocationsCommand, Response<string>>
    {
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ValidateOrganizationLocationsCommandHandler(ResponseHandler responseHandler, IStringLocalizer<SharedResources> localizer)
        {
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(ValidateOrganizationLocationsCommand request, CancellationToken cancellationToken)
        {
            return _responseHandler.Success<string>(null, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}
