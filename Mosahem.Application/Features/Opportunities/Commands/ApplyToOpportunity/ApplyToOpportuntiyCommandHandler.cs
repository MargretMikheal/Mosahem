using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Entities.Questions;
using System.Text.Json;

namespace Mosahem.Application.Features.Opportunities.Commands.ApplyToOpportunity
{
    public class ApplyToOpportunityCommandHandler : IRequestHandler<ApplyToOpportunityCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ApplyToOpportunityCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(ApplyToOpportunityCommand request, CancellationToken cancellationToken)
        {
            //Check the volunteer exists
            var volunteer = await _unitOfWork.Volunteers.GetByIdAsync(request.VolunteerId, cancellationToken);

            if (volunteer == null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.User.NotFound]);

            // Check if the volunteer has already applied to this opportunity
            var applicationExists = await _unitOfWork.OpportunityApplications
                .IsExist(request.VolunteerId, request.OpportunityId, cancellationToken);

            if (applicationExists)
                return _responseHandler.BadRequest<string>(_localizer[SharedResourcesKeys.Application.AlreadyApplied]);

            //Check that all the questions exist for the opportunity and it belongs to the same opportunity
            var allQuestionsExist = await _unitOfWork.Questions
                .DoAllQuestionsExistForOpportunity(request.Answers.Select(a => a.QuestionId).ToList(), request.OpportunityId, cancellationToken);

            if (!allQuestionsExist)
                return _responseHandler.BadRequest<string>(_localizer[SharedResourcesKeys.Application.InvalidQuestions]);

            var answers = request.Answers.Select(a => new QuestionAnswer
            {
                VolunteerId = request.VolunteerId,
                QuestionId = a.QuestionId,
                AnswerText = a.AnswerText,
                ChoiceKey = a.ChoiceKey,
                Json = JsonDocument.Parse(JsonSerializer.SerializeToUtf8Bytes(a.SelectedChoices))
            }).ToList();

            var application = request.Adapt<OpportunityApplication>();

            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _unitOfWork.QuestionAnswers.AddRangeAsync(answers, cancellationToken);
                await _unitOfWork.OpportunityApplications.AddAsync(application, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return _responseHandler.Success<string>(_localizer[SharedResourcesKeys.Application.AppliedSuccessfully]);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>>
                {
                    {  "Exception", new List<string> { ex.Message }   }
                });
            }
        }
    }
}
