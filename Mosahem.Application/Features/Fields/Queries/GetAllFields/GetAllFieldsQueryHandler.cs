using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Fields.Queries.GetAllFields
{
    public class GetAllFieldsQueryHandler : IRequestHandler<GetAllFieldsQuery, Response<IReadOnlyList<GetAllFieldsQueryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public GetAllFieldsQueryHandler(
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

        public async Task<Response<IReadOnlyList<GetAllFieldsQueryResponse>>> Handle(GetAllFieldsQuery request, CancellationToken cancellationToken)
        {
            var fields = await _unitOfWork.Fields.GetAllAsync(cancellationToken);

            var response = _mapper.Map<IReadOnlyList<GetAllFieldsQueryResponse>>(fields);

            return _responseHandler.Success(response);
        }
    }
}
