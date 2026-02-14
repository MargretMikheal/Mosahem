using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities;

namespace Mosahem.Application.Features.Organization.Queries.GetOrganizationFields
{
    public class GetOrganizationFieldsQueryHandler : IRequestHandler<GetOrganizationFieldsQuery, Response<List<GetOrganizationFieldsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public GetOrganizationFieldsQueryHandler(
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

        public async Task<Response<List<GetOrganizationFieldsResponse>>> Handle(GetOrganizationFieldsQuery request, CancellationToken cancellationToken)
        {
            var organizationFields = await _unitOfWork.Repository<OrganizationField>()
                .GetTableNoTracking()
                .Where(of => of.OrganizationId == request.OrganizationId)
                .Include(of => of.Field)
                .ToListAsync(cancellationToken);

            if (!organizationFields.Any())
                return _responseHandler.NotFound<List<GetOrganizationFieldsResponse>>(_localizer[SharedResourcesKeys.Validation.NotFound]);

            var response = _mapper.Map<List<GetOrganizationFieldsResponse>>(organizationFields);

            return _responseHandler.Success(response, _localizer[SharedResourcesKeys.General.Success]);
        }
    }
}
