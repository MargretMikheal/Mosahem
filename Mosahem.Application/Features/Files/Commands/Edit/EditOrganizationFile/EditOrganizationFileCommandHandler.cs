using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Entities.Profiles;
using mosahem.Domain.Enums;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Files.Commands.Edit.EditOrganizationFile
{
    public class EditOrganizationFileCommandHandler : IRequestHandler<EditOrganizationFileCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IFileOwnerService<Organization> _organizationFileService;
        public EditOrganizationFileCommandHandler(
            IUnitOfWork unitOfWork,
            IFileService fileService,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IFileOwnerService<Organization> organizationFileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _organizationFileService = organizationFileService;
        }

        public async Task<Response<string>> Handle(EditOrganizationFileCommand request, CancellationToken cancellationToken)
        {
            var generalError = _localizer[SharedResourcesKeys.General.OperationFailed].Value;

            StorageFolder folder = Enum.Parse<StorageFolder>(request.FolderName, ignoreCase: true);

            var organization = await _unitOfWork.Organizations.GetByIdAsync(request.OrganizationId, cancellationToken);
            if (organization is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);

            Opportunity? opportunity = null;
            if (request.OpportunityId.HasValue)
            {
                opportunity = await _unitOfWork.Opportunities.GetByIdAsync((Guid)request.OpportunityId, cancellationToken);

                if (opportunity is null || opportunity.OrganizationId != organization.Id)
                    return _responseHandler.NotFound<string>(
                        generalError,
                        new Dictionary<string, List<string>>
                        {
                                {"Opportunity"  , new(){_localizer[SharedResourcesKeys.Validation.NotFound]} }
                        });
            }
            try
            {
                var canEdit = _organizationFileService.CanEditFileAsync(organization, folder, opportunity);
                if (!canEdit)
                    return _responseHandler.BadRequest<string>(generalError);

                var oldKey = _organizationFileService.GetOldFileKeyAsync(organization, folder, opportunity);

                var newKey = await _fileService.EditFileAsync(oldKey, request.File, request.FolderName, cancellationToken);

                _organizationFileService.SetNewFileKeyAsync(organization, folder, newKey, opportunity);

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