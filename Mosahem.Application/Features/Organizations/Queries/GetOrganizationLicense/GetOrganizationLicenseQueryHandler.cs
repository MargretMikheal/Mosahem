using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationLicense
{
    public class GetOrganizationLicenseQueryHandler : IRequestHandler<GetOrganizationLicenseQuery, Response<GetOrganizationLicenseResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public GetOrganizationLicenseQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IMapper mapper,
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<Response<GetOrganizationLicenseResponse>> Handle(GetOrganizationLicenseQuery request, CancellationToken cancellationToken)
        {
            var organization = await _unitOfWork.Organizations.GetByIdAsync(request.OrganizationId, cancellationToken);

            if (organization is null)
                return _responseHandler.NotFound<GetOrganizationLicenseResponse>(_localizer[SharedResourcesKeys.User.NotFound]);

            var licenseUrl = _fileService.GetFileUrl(organization.LicenseKey, isPrivate: true);
            var response = new GetOrganizationLicenseResponse() { LicenseUrl = licenseUrl };
            return _responseHandler.Success(response);
        }
    }
}
