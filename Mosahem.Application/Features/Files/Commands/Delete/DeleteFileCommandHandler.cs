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

        public DeleteFileCommandHandler(
            IFileService fileService,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _fileService = fileService;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.FileKey))
            {
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>> { { nameof(request.FileKey), new List<string> { _localizer[SharedResourcesKeys.Validation.Required] } } });
            }

            try
            {
                await _fileService.DeleteFileAsync(request.FileKey);
                return _responseHandler.Deleted<string>();
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>> { { "Delete", new List<string> { ex.Message } } });
            }
        }
    }
}