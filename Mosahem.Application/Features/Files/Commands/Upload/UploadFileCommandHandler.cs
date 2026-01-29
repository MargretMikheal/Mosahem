using MediatR;
using mosahem.Application.Common;
using Mosahem.Application.Features.Files.Commands.Upload;
using Mosahem.Application.Interfaces;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Response<string>>
{
    private readonly IFileService _fileService;
    private readonly ResponseHandler _responseHandler;

    public UploadFileCommandHandler(IFileService fileService, ResponseHandler responseHandler)
    {
        _fileService = fileService;
        _responseHandler = responseHandler;
    }

    public async Task<Response<string>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var fileUrl = await _fileService.UploadFileAsync(request.File, request.FolderName, cancellationToken);
        return _responseHandler.Success(fileUrl);
    }
}