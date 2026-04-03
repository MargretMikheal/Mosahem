using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Authentication.Commands.ValidateVolunteerBasicInfo
{
    public class ValidateVolunteerBasicInfoCommandHandler : IRequestHandler<ValidateVolunteerBasicInfoCommand, Response<string>>
    {
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ValidateVolunteerBasicInfoCommandHandler(ResponseHandler responseHandler, IStringLocalizer<SharedResources> localizer)
        {
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(ValidateVolunteerBasicInfoCommand request, CancellationToken cancellationToken)
        {
            return _responseHandler.Success<string>(null, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}
