using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Governates.GetAllGovernates
{
    public class GetAllGovernatesQueryHandler : IRequestHandler<GetAllGovernatesQuery, Response<IReadOnlyList<GetAllGovernatesQueryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public GetAllGovernatesQueryHandler(
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

        public async Task<Response<IReadOnlyList<GetAllGovernatesQueryResponse>>> Handle(GetAllGovernatesQuery request, CancellationToken cancellationToken)
        {
            var governates = await _unitOfWork.Governorates.GetAllAsync(cancellationToken);

            var response = _mapper.Map<IReadOnlyList<GetAllGovernatesQueryResponse>>(governates);

            return _responseHandler.Success(response);
        }
    }
}
