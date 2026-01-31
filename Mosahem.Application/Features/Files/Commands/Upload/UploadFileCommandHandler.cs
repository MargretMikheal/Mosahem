using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Resources;
using Mosahem.Application.Features.Files.Commands.Upload;
using Mosahem.Application.Interfaces;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Response<string>>
{
    private readonly IFileService _fileService;
    private readonly ResponseHandler _responseHandler;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public UploadFileCommandHandler(
        IFileService fileService,
        ResponseHandler responseHandler,
        IStringLocalizer<SharedResources> localizer)
    {
        _fileService = fileService;
        _responseHandler = responseHandler;
        _localizer = localizer;
    }

    public async Task<Response<string>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var fileUrl = await _fileService.UploadFileAsync(request.File, request.FolderName, cancellationToken);
            return _responseHandler.Success(fileUrl);
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest<string>(
                _localizer[SharedResourcesKeys.General.OperationFailed],
                new Dictionary<string, List<string>> { { "Upload", new List<string> { ex.Message } } });
        }
    }
}