using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Volunteers.Commands.EditVolunteerBasicInfoCommand
{
    public class EditVolunteerBasicInfoCommandHandler : IRequestHandler<EditVolunteerBasicInfoCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EditVolunteerBasicInfoCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(EditVolunteerBasicInfoCommand request, CancellationToken cancellationToken)
        {
            // Get volunteer
            var volunteer = await _unitOfWork.Volunteers
                .GetByIdAsync(request.VolunteerId, cancellationToken);

            if (volunteer is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);

            var errors = new Dictionary<string, List<string>>();

            // NationalId: one-time set only
            if (request.NationalId is not null && volunteer.NationalId is not null)
            {
                errors.TryAdd(nameof(request.NationalId),
                    new List<string> { _localizer[SharedResourcesKeys.Validation.CannotEditThisField] });
            }

            // DateOfBirth: one-time set only
            if (request.DateOfBirth is not null && volunteer.DateOfBirth is not null)
            {
                errors.TryAdd(nameof(request.DateOfBirth),
                    new List<string> { _localizer[SharedResourcesKeys.Validation.CannotEditThisField] });
            }

            // Gender: one-time set only
            if (request.Gender is not null && volunteer.Gender is not null)
            {
                errors.TryAdd(nameof(request.Gender),
                    new List<string> { _localizer[SharedResourcesKeys.Validation.CannotEditThisField] });
            }

            // Return all one-time-set errors at once before saving
            if (errors.Count > 0)
                return _responseHandler.BadRequest<string>(null!, errors);

            request.Adapt(volunteer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success<string>(_localizer[SharedResourcesKeys.General.Updated]);
        }
    }
}
