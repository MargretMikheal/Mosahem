using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Questions;

namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunityQuestions
{
    public class EditOpportunityQuestionsCommandHandler : IRequestHandler<EditOpportunityQuestionsCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EditOpportunityQuestionsCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(EditOpportunityQuestionsCommand request, CancellationToken cancellationToken)
        {
            var opportunity = await _unitOfWork.Opportunities
                .GetTableAsTracking()
                .Include(o => o.Questions)
                .FirstOrDefaultAsync(o => o.Id == request.OpportunityId, cancellationToken);

            if (opportunity is null || opportunity.OrganizationId != request.OrganizationId)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        { "OpportunityId" , new(){ _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });

            var haveIdQuestionsIds = request.Questions
                .Where(q => q.QuestionId != null)
                .Select(q => q.QuestionId!.Value)
                .ToList();

            var existingIds = opportunity.Questions?
                .Select(q => q.Id)
                .ToHashSet() ?? new HashSet<Guid>();

            var invalidIdsExists = haveIdQuestionsIds
                .Any(qId => !existingIds.Contains(qId));

            if (invalidIdsExists)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        { nameof(request.Questions) , new(){ _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });

            if (haveIdQuestionsIds.Count != haveIdQuestionsIds.Distinct().Count())
            {
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.Validation.DuplicatedData]);
            }

            opportunity.Questions = opportunity.Questions ?? new List<Question>();


            var existingQuestionsRequest = request.Questions
              .Where(q => q.QuestionId != null)
              .ToList();

            var existingQuestionsRequestIds = request.Questions.Where(q => q.QuestionId != null)
              .Select(q => q.QuestionId).ToHashSet();

            var newQuestionsRequest = request.Questions
                .Where(q => q.QuestionId == null)
                .ToList();

            var QuestionsDict = opportunity.Questions
                .ToDictionary(q => q.Id);

            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Delete Questions
                var toDelete = opportunity.Questions.Where(q => !existingQuestionsRequestIds.Contains(q.Id)).ToList();

                await _unitOfWork.Questions.DeleteRangeAsync(toDelete, cancellationToken);
                var questionsCount = 0;
                //Put Questions
                foreach (var reqQ in existingQuestionsRequest)
                {
                    var existingQ = QuestionsDict[reqQ.QuestionId!.Value];
                    existingQ.Order = ++questionsCount;
                    reqQ.Adapt(existingQ);
                }

                // Add Questions
                var toAdd = newQuestionsRequest.Adapt<List<Question>>();
                foreach (var question in toAdd)
                {
                    question.OpportunityId = request.OpportunityId;
                    question.Order = ++questionsCount;
                }
                await _unitOfWork.Questions.AddRangeAsync(toAdd, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return _responseHandler.Success<string>(null!, _localizer[SharedResourcesKeys.General.Updated]);

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>>
                    {
                        {"Exception" , new(){ ex.Message} }
                    });
            }
        }
    }
}
