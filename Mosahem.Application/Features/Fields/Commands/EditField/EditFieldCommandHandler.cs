using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Fields.Commands.EditField
{
    public class EditFieldCommandHandler : IRequestHandler<EditFieldCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public EditFieldCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(EditFieldCommand request, CancellationToken cancellationToken)
        {
            var field = await _unitOfWork.Fields.GetByIdAsync(request.Id);
            // Check if the field exists
            if (field is null)
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>>
                    {
                        { "FieldId" ,new List<string> { _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });

            request.Adapt(field);
            await _unitOfWork.Fields.UpdateAsync(field);
            await _unitOfWork.SaveChangesAsync();

            return _responseHandler.Success<string>(_localizer[SharedResourcesKeys.General.Updated]);
        }
    }
}
