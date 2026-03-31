using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organizations.Commands.EditOrganizationAboutUs
{
    public class EditOrganizationAboutUsCommandHandler : IRequestHandler<EditOrganizationAboutUsCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EditOrganizationAboutUsCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(EditOrganizationAboutUsCommand request, CancellationToken cancellationToken)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(request.OrganizationId, cancellationToken);
            if (organization == null)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                         { "OrganizationId" , new(){ _localizer[SharedResourcesKeys.User.NotFound] } }
                    });
            organization.AboutUs = request.AboutUs;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _responseHandler.Success<string>(null!, _localizer[SharedResourcesKeys.General.Updated]);
        }
    }
}
