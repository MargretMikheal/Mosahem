using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Files.Commands.Delete
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, Response<string>>
    {
        private readonly IFileService _fileService;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public DeleteFileCommandHandler(IFileService fileService, ResponseHandler responseHandler, IStringLocalizer<SharedResources> localizer)
        {
            _fileService = fileService;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.FileUrl))
                return _responseHandler.BadRequest<string>(_localizer[SharedResourcesKeys.Validation.Required]);

            await _fileService.DeleteFileAsync(request.FileUrl);
            return _responseHandler.Deleted<string>();
        }
    }
}
