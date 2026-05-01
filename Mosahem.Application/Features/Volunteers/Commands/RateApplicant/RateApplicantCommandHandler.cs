using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Volunteers.Commands.RateApplicant
{
    public class RateApplicantCommandHandler : IRequestHandler<RateApplicantCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ResponseHandler _responseHandler;
        public RateApplicantCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResources> localizer,
            ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _responseHandler = responseHandler;
        }

        public async Task<Response<string>> Handle(RateApplicantCommand request, CancellationToken cancellationToken)
        {
            // Get Organization
            var organization = await _unitOfWork.Organizations.GetByIdAsync(request.OrganizationId, cancellationToken);

            if (organization is null)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        {"OrganizationId" , new List<string> {_localizer[SharedResourcesKeys.User.NotFound]} }
                    });



            // Check application exists and volunteer is Accepted
            var application = await _unitOfWork.OpportunityApplications.GetByVolunteerAndOpportunityIdAsync(request.VolunteerId, request.OpportunityId, cancellationToken);

            bool belongsToOrganization = application?.Opportunity.OrganizationId != request.OrganizationId;

            if (application is null || !belongsToOrganization)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        {"Application" , new List<string> {_localizer[SharedResourcesKeys.Application.ApplicationNotFound]} }
                    });

            if (application.ApplicantStatus is not ApplicantStatus.Accepted)
                return _responseHandler.BadRequest<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        {"Application" , new List<string> {_localizer[SharedResourcesKeys.Application.CannotRateUnacceptedApplicant]} }
                    });

            var opportunityStatus = application.Opportunity.Status;
            if (!opportunityStatus.HasFlag(OpportunityStatus.Ended))
                return _responseHandler.BadRequest<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        {"Opportunity" , new List<string> {_localizer[SharedResourcesKeys.Application.ApplicantCanBeRatedOnlyAfterTheOpportunityEnds]} }
                    });

            if (application.Rating.HasValue)
                return _responseHandler.BadRequest<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        {"Application" , new List<string> {_localizer[SharedResourcesKeys.Application.AlreadyRated]} }

                    });

            // Save rating
            application.Rating = request.Rate;

            await _unitOfWork.SaveChangesAsync();

            return _responseHandler.Success<string>(_localizer[SharedResourcesKeys.Application.ApplicantRatedSuccessfully]);
        }
    }
}
