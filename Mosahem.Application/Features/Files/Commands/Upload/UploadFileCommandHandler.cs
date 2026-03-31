using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Features.Files.Commands.Upload;
using Mosahem.Application.Interfaces;
using Mosahem.Domain.Entities;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, Response<string>>
{
    private readonly IFileService _fileService;
    private readonly ResponseHandler _responseHandler;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IUnitOfWork _unitOfWork;
    public UploadFileCommandHandler(
        IFileService fileService,
        ResponseHandler responseHandler,
        IStringLocalizer<SharedResources> localizer,
        IUnitOfWork unitOfWork)
    {
        _fileService = fileService;
        _responseHandler = responseHandler;
        _localizer = localizer;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var fileKey = await _fileService.UploadFileAsync(request.File, request.FolderName, cancellationToken);
            await _unitOfWork.Repository<TemporaryFileUpload>().AddAsync(new TemporaryFileUpload
            {
                Id = Guid.NewGuid(),
                FileKey = fileKey,
                FolderName = request.FolderName,
                CreatedAt = DateTime.UtcNow
            }, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _responseHandler.Success(fileKey);
        }
        catch (Exception ex)
        {
            return _responseHandler.BadRequest<string>(
                _localizer[SharedResourcesKeys.General.OperationFailed],
                new Dictionary<string, List<string>> { { "Upload", new List<string> { ex.Message } } });
        }
    }
}