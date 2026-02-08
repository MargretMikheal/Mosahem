using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.MasterData;

namespace Mosahem.Application.Features.Fields.Commands.AddField
{
    public class AddFieldCommandHandler : IRequestHandler<AddFieldCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public AddFieldCommandHandler(
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
        public async Task<Response<string>> Handle(AddFieldCommand request, CancellationToken cancellationToken)
        {
            var field = _mapper.Map<Field>(request);
            try
            {
                await _unitOfWork.Fields.AddAsync(field, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return _responseHandler.Created<string>(_localizer[SharedResourcesKeys.Success.Added]);
            }
            catch (Exception ex)
            {
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
