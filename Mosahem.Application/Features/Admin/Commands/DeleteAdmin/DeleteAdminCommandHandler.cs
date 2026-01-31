using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace mosahem.Application.Features.Admin.Commands.DeleteAdmin
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public DeleteAdminCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            var generalError = _localizer[SharedResourcesKeys.General.OperationFailed].Value;

            if (request.AdminId == request.CurrentUserId)
            {
                return _responseHandler.BadRequest<string>(
                    generalError,
                    new Dictionary<string, List<string>>
                    {
                        { "AdminId", new List<string> { _localizer[SharedResourcesKeys.Validation.CannotDeleteSelf] } }
                    });
            }

            var user = await _unitOfWork.Users.GetByIdAsync(request.AdminId);

            if (user == null || user.IsDeleted)
            {
                return _responseHandler.BadRequest<string>(
                    generalError,
                    new Dictionary<string, List<string>>
                    {
                        { "AdminId", new List<string> { _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });
            }

            if (user.Role != UserRole.Admin)
            {
                return _responseHandler.BadRequest<string>(
                    generalError,
                    new Dictionary<string, List<string>>
                    {
                        { "Role", new List<string> { "Target user is not an admin." } }
                    });
            }

            try
            {
                user.IsDeleted = true;
                user.DeletedAt = DateTime.UtcNow;

                await _unitOfWork.Users.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return _responseHandler.Deleted<string>();
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<string>(
                    generalError,
                    new Dictionary<string, List<string>>
                    {
                        { "Exception", new List<string> { ex.Message } }
                    });
            }
        }
    }
}