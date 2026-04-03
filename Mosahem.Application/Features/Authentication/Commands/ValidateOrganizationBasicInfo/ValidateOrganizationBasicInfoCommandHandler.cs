using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Resources;

namespace mosahem.Application.Features.Authentication.Commands.ValidateOrganizationBasicInfo
{
    public class ValidateOrganizationBasicInfoCommandHandler : IRequestHandler<ValidateOrganizationBasicInfoCommand, Response<string>>
    {
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ValidateOrganizationBasicInfoCommandHandler(ResponseHandler responseHandler, IStringLocalizer<SharedResources> localizer)
        {
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(ValidateOrganizationBasicInfoCommand request, CancellationToken cancellationToken)
        {
            return _responseHandler.Success<string>(null, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}