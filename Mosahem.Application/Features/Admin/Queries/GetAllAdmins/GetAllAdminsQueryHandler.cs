using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Admin.Queries.GetAllAdmins
{
    public class GetAllAdminsQueryHandler : IRequestHandler<GetAllAdminsQuery, Response<IReadOnlyList<GetAllAdminsQueryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public GetAllAdminsQueryHandler(
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

        public async Task<Response<IReadOnlyList<GetAllAdminsQueryResponse>>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
        {
            var admins = await _unitOfWork.Users.GetUsersByRole(UserRole.Admin, cancellationToken);

            var response = _mapper.Map<IReadOnlyList<GetAllAdminsQueryResponse>>(admins);

            return _responseHandler.Success(response);
        }
    }
}
