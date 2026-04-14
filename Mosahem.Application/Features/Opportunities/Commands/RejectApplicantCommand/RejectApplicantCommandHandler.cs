using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;
using Mosahem.Application.Interfaces;
using Mosahem.Domain.AppMetaData;

namespace Mosahem.Application.Features.Opportunities.Commands.RejectApplicantCommand
{
    public class RejectApplicantCommandHandler : IRequestHandler<RejectApplicantCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;

        public RejectApplicantCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer,
            IEmailService emailService,
            IEmailTemplateService emailTemplateService,
            IHttpContextAccessor httpContextAccessor,
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }

        public async Task<Response<string>> Handle(RejectApplicantCommand request, CancellationToken cancellationToken)
        {
            // Check organization exists
            var organization = await _unitOfWork.Organizations
                .GetByIdAsync(request.OrganizationId, cancellationToken);

            if (organization is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);

            // Check opportunity belongs to organization
            var opportunityExistInOrganization = await _unitOfWork.Opportunities
                .IsOwnedByOrganizationAsync(request.OpportunityId, request.OrganizationId, cancellationToken);

            if (!opportunityExistInOrganization)
                return _responseHandler.NotFound<string>(null!,
                    new Dictionary<string, List<string>>
                    {
                        { nameof(request.OpportunityId), new List<string> { _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });

            // Get application
            var application = await _unitOfWork.OpportunityApplications
                .GetByVolunteerAndOpportunityIdAsync(request.ApplicantId, request.OpportunityId, cancellationToken);

            if (application is null)
                return _responseHandler.NotFound<string>(null!,
                    new Dictionary<string, List<string>>
                    {
                        { "Application", new List<string> { _localizer[SharedResourcesKeys.Application.ApplicationNotFound] } }
                    });

            // Guard: already rejected
            if (application.ApplicantStatus == ApplicantStatus.Rejected)
                return _responseHandler.BadRequest<string>(null!,
                    new Dictionary<string, List<string>>
                    {
                        { "Application", new List<string> { _localizer[SharedResourcesKeys.Application.AlreadyRejected] } }
                    });

            // Guard: already accepted — cannot reject an accepted applicant
            if (application.ApplicantStatus == ApplicantStatus.Accepted)
                return _responseHandler.BadRequest<string>(null!,
                    new Dictionary<string, List<string>>
                    {
                        { "Application", new List<string> { _localizer[SharedResourcesKeys.Application.AlreadyAccepted] } }
                    });

            // Update status
            application.ApplicantStatus = ApplicantStatus.Rejected;

            var httpRequest = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = $"{httpRequest.Scheme}://{httpRequest.Host}";
            var opportunitiesUrl = $"{baseUrl}/{Router.OpportunityRouting.GetAll}";
            var opportunityImageUrl = _fileService.GetFileUrl(application.Opportunity.PhotoKey, true);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            await _emailService.SendEmailAsync(
                to: application.Volunteer.User.Email,
                subject: $"Mosahem | Update on your application for {application.Opportunity.Title}",
                body: _emailTemplateService.GenerateVolunteerRejectionEmail(
                    application.Volunteer.User.FullName,
                    application.Opportunity.Title,
                    opportunitiesUrl,
                    opportunityImageUrl)
            );

            return _responseHandler.Success<string>(_localizer[SharedResourcesKeys.Success.ApplicantRejectedSuccessfully]);
        }
    }
}
