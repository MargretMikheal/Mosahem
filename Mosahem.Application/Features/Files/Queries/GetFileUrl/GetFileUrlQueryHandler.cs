using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Files.Queries.GetFileUrl
{
    public class GetFileUrlQueryHandler : ResponseHandler, IRequestHandler<GetFileUrlQuery, Response<string>>
    {
        private readonly IFileService _fileService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public GetFileUrlQueryHandler(IFileService fileService, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _fileService = fileService;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(GetFileUrlQuery request, CancellationToken cancellationToken)
        {
            var url = _fileService.GetFileUrl(request.Key, request.IsPrivate);

            if (string.IsNullOrEmpty(url))
                return BadRequest<string>(_localizer[SharedResourcesKeys.Validation.NotFound]);

            return Success(url);
        }
    }
}