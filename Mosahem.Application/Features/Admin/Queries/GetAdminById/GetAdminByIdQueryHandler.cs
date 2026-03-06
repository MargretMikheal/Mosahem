using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Admin.Queries.GetAdminById
{
    public class GetAdminByIdQueryHandler : IRequestHandler<GetAdminByIdQuery, Response<GetAdminByIdQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public GetAdminByIdQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<GetAdminByIdQueryResponse>> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            var generalError = _localizer[SharedResourcesKeys.General.OperationFailed].Value;

            var user = await _unitOfWork.Users.GetByIdAsync(request.AdminId, cancellationToken);
            if (user is null || user.Role is not UserRole.Admin)
                return _responseHandler.NotFound<GetAdminByIdQueryResponse>(_localizer[SharedResourcesKeys.User.NotFound]);

            var response = user.Adapt<GetAdminByIdQueryResponse>();

            return _responseHandler.Success(response);
        }
    }
}
