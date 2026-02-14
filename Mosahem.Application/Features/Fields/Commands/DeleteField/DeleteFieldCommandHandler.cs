using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Fields.Commands.DeleteField
{
    public class DeleteFieldCommandHandler : IRequestHandler<DeleteFieldCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public DeleteFieldCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }
        public async Task<Response<string>> Handle(DeleteFieldCommand request, CancellationToken cancellationToken)
        {
            var field = await _unitOfWork.Fields.GetByIdAsync(request.Id, cancellationToken);
            // Check if the field exists
            if (field is null)
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>>
                    {
                        { "FieldId" ,new List<string> { _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });

            await _unitOfWork.Fields.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Deleted<string>();
        }
    }
}
