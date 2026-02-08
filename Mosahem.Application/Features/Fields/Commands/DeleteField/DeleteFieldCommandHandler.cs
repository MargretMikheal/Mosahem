using MapsterMapper;
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
        private readonly IMapper _mapper;

        public DeleteFieldCommandHandler(
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
        public async Task<Response<string>> Handle(DeleteFieldCommand request, CancellationToken cancellationToken)
        {
            var field = await _unitOfWork.Fields.GetByIdAsync(request.Id);
            if (field is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.Validation.NotFound]);

            await _unitOfWork.Fields.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Deleted<string>();
        }
    }
}
