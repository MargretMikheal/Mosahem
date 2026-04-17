using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Opportunities.Queries.GetQuestionsAnswers
{
    public class GetQuestionsAnswersQueryHandler : IRequestHandler<GetQuestionAnswersQuery, Response<IReadOnlyList<GetQuestionsAnswerResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public GetQuestionsAnswersQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<IReadOnlyList<GetQuestionsAnswerResponse>>> Handle(
            GetQuestionAnswersQuery request,
            CancellationToken cancellationToken)
        {
            // Check for the organization
            if (request.OrganizationId.HasValue)
            {
                var organization = await _unitOfWork.Organizations
                    .GetByIdAsync(request.OrganizationId.Value, cancellationToken);

                if (organization is null)
                    return _responseHandler.NotFound<IReadOnlyList<GetQuestionsAnswerResponse>>(
                        _localizer[SharedResourcesKeys.User.NotFound]);

                var owendByOrganization = await _unitOfWork.Opportunities.IsOwnedByOrganizationAsync(request.OpportunityId, request.OrganizationId.Value, cancellationToken);
                if (!owendByOrganization)
                    return _responseHandler.NotFound<IReadOnlyList<GetQuestionsAnswerResponse>>(
                        null!,
                        new Dictionary<string, List<string>>
                        {
                            {"Opportunity" , new List<string> { _localizer[SharedResourcesKeys.Validation.NotFound] }  }
                        });
            }
            // Check volunteer exists
            var volunteer = await _unitOfWork.Volunteers.GetByIdAsync(request.VolunteerId, cancellationToken);
            if (volunteer is null)
                return _responseHandler.NotFound<IReadOnlyList<GetQuestionsAnswerResponse>>(
                    _localizer[SharedResourcesKeys.User.NotFound]);

            // Check volunteer has applied to this opportunity
            var applicationExists = await _unitOfWork.OpportunityApplications
                .IsExistAsync(request.VolunteerId, request.OpportunityId, cancellationToken);
            if (!applicationExists)
                return _responseHandler.NotFound<IReadOnlyList<GetQuestionsAnswerResponse>>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        {"Application" , new List<string> { _localizer[SharedResourcesKeys.Validation.NotFound] }  }
                    });

            // Get all questions for the opportunity with their answers
            var questionAnswers = await _unitOfWork.QuestionAnswers.GetWithDetailsAsync(request.VolunteerId, request.OpportunityId, cancellationToken);

            var response = questionAnswers.Adapt<IReadOnlyList<GetQuestionsAnswerResponse>>();

            return _responseHandler.Success(response);
        }
    }
}
