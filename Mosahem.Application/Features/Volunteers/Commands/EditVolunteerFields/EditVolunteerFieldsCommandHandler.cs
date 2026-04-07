using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities;

namespace Mosahem.Application.Features.Volunteers.Commands.EditVolunteerFields
{
    public class EditVolunteerFieldsCommandHandler : IRequestHandler<EditVolunteerFieldsCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EditVolunteerFieldsCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(EditVolunteerFieldsCommand request, CancellationToken cancellationToken)
        {
            // Check volunteer existence
            var volunteer = await _unitOfWork.Volunteers.GetByIdAsync(request.VolunteerId, cancellationToken);
            if (volunteer is null)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        { "VolunteerId", new() { _localizer[SharedResourcesKeys.User.NotFound] } }
                    });

            // Get current volunteer fields
            var currentVolunteerFields = await _unitOfWork.Repository<VolunteerField>()
                .GetTableAsTracking()
                .Where(vf => vf.VolunteerId == request.VolunteerId)
                .ToHashSetAsync(cancellationToken);

            // Diff — what to add and what to delete
            var toAdd = request.FieldIds
                .Where(fId => currentVolunteerFields.All(vf => vf.FieldId != fId))
                .Select(fId => new VolunteerField
                {
                    VolunteerId = request.VolunteerId,
                    FieldId = fId,
                })
                .ToList();

            var toDelete = currentVolunteerFields
                .Where(vf => !request.FieldIds.Contains(vf.FieldId))
                .ToList();

            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _unitOfWork.Repository<VolunteerField>().AddRangeAsync(toAdd, cancellationToken);
                await _unitOfWork.Repository<VolunteerField>().DeleteRangeAsync(toDelete, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return _responseHandler.Success<string>(null!, _localizer[SharedResourcesKeys.General.Updated]);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>>
                    {
                        { "Exception", new() { ex.Message } }
                    });
            }
        }
    }
}
