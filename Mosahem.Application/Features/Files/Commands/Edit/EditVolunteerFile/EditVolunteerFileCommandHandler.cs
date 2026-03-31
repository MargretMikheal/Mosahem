using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Profiles;
using mosahem.Domain.Enums;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Files.Commands.Edit.EditVolunteerFile
{
    public class EditVolunteerFileCommandHandler : IRequestHandler<EditVolunteerFileCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IFileOwnerService<Volunteer> _volunteerFileService;

        public EditVolunteerFileCommandHandler(
            IUnitOfWork unitOfWork,
            IFileService fileService,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IFileOwnerService<Volunteer> volunteerFileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _volunteerFileService = volunteerFileService;
        }

        public async Task<Response<string>> Handle(EditVolunteerFileCommand request, CancellationToken cancellationToken)
        {
            var generalError = _localizer[SharedResourcesKeys.General.OperationFailed].Value;

            var volunteer = await _unitOfWork.Volunteers.GetByIdAsync(request.VolunteerId, cancellationToken);
            if (volunteer is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);

            StorageFolder folder = Enum.Parse<StorageFolder>(request.FolderName, ignoreCase: true);

            try
            {

                var canEdit = _volunteerFileService.CanEditFileAsync(volunteer, folder, null);
                if (!canEdit)
                    return _responseHandler.BadRequest<string>(generalError);

                var oldKey = _volunteerFileService.GetOldFileKeyAsync(volunteer, folder, null);

                var newKey = await _fileService.EditFileAsync(oldKey, request.File, request.FolderName, cancellationToken);

                _volunteerFileService.SetNewFileKeyAsync(volunteer, folder, newKey, null);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<string>(
                    generalError,
                    new Dictionary<string, List<string>>
                    {
                            {"Exception"  , new(){_localizer[ex.Message]} }
                    });
            }
            return _responseHandler.Success<string>(_localizer[SharedResourcesKeys.General.Updated]);

        }
    }
}
