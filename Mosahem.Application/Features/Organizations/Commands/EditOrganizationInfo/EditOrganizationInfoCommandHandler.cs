using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organizations.Commands.EditOrganizationInfo
{
    public class EditOrganizationInfoCommandHandler : IRequestHandler<EditOrganizationInfoCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EditOrganizationInfoCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(EditOrganizationInfoCommand request, CancellationToken cancellationToken)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(request.OrganizationId, cancellationToken);
            if (organization is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);

            var user = await _unitOfWork.Users.GetByIdAsync(request.OrganizationId, cancellationToken);
            if (user is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);

            //Begin Transaction
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                //Mapping
                request.Adapt(organization);
                request.Adapt(user);


                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return _responseHandler.Success<string>(_localizer[SharedResourcesKeys.General.Updated]);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>>
                    {
                        { "Exception", new List<string> { ex.Message } }
                    });
            }
        }
    }
}
