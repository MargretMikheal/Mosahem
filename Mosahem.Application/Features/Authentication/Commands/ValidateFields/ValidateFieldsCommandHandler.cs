using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Authentication.Commands.ValidateFields
{
    public class ValidateFieldsCommandHandler : IRequestHandler<ValidateFieldsCommand, Response<string>>
    {
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ValidateFieldsCommandHandler(ResponseHandler responseHandler, IStringLocalizer<SharedResources> localizer)
        {
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(ValidateFieldsCommand request, CancellationToken cancellationToken)
        {
            return _responseHandler.Success<string>(null, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}
