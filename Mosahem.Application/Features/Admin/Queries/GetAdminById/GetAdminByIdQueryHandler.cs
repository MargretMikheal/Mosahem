using MapsterMapper;
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
        private readonly IMapper _mapper;

        public GetAdminByIdQueryHandler(
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

        public async Task<Response<GetAdminByIdQueryResponse>> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
        {
            var generalError = _localizer[SharedResourcesKeys.General.OperationFailed].Value;

            var user = await _unitOfWork.Users.GetByIdAsync(request.AdminId, cancellationToken);
            if (user is null || user.IsDeleted)
            {
                return _responseHandler.BadRequest<GetAdminByIdQueryResponse>(
                    generalError,
                    new Dictionary<string, List<string>>
                    {
                        { "AdminId", new List<string> { _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });
            }

            if (user.Role is not UserRole.Admin)
            {
                return _responseHandler.BadRequest<GetAdminByIdQueryResponse>(
                    generalError,
                    new Dictionary<string, List<string>>
                    {
                        { "Role", new List<string> { "Target user is not an admin." } }
                    });
            }

            var response = _mapper.Map<GetAdminByIdQueryResponse>(user);

            return _responseHandler.Success(response);
        }
    }
}
