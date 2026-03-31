using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunitySkills
{
    public class EditOpportunitySkillsCommandHandler : IRequestHandler<EditOpportunitySkillsCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public EditOpportunitySkillsCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(EditOpportunitySkillsCommand request, CancellationToken cancellationToken)
        {
            /*
             * check for opportunity existance
             * check if the opportunity belongs to that organization
             */
            var opportunity = await _unitOfWork.Opportunities.GetByIdAsync(request.OpportunityId, cancellationToken);

            if (opportunity is null || opportunity.OrganizationId != request.OrganizationId)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        { "OpportunityId" , new(){ _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });

            //Check if the start date is after 48 hours or more
            if (opportunity.StartDate.AddHours(-48) < DateTime.UtcNow)
                return _responseHandler.BadRequest<string>(_localizer[SharedResourcesKeys.Validation.SkillsCanBeEditedOnlyBefore48Houres]);

            //Check if all the skills exist
            var existingSkillsIds = (await _unitOfWork.Skills.GetAllAsync(cancellationToken)).Select(s => s.Id).ToHashSet();


            var invalidSkillIdExist = request.SkillsIds.Any(SId => !existingSkillsIds.Contains(SId));
            if (invalidSkillIdExist)
                return _responseHandler.NotFound<string>(
                    null!,
                    new Dictionary<string, List<string>>
                    {
                        { "SkillsIds" , new(){ _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });

            var skillType = Enum.Parse<OpportunitySkillType>(request.SkillType, ignoreCase: true);

            //Delete the old skills by skill type
            var opportunitySkills = await _unitOfWork.Repository<OpportunitySkill>()
                .GetTableAsTracking()
                .Where(os => os.OpportunityId == request.OpportunityId)
                .ToHashSetAsync();

            var isIntersected = opportunitySkills.Any(s => request.SkillsIds.Contains(s.SkillId) && s.SkillType != skillType);
            if (isIntersected)
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>>
                    {
                        { "SkillsIds" , new(){ _localizer[SharedResourcesKeys.Validation.SkillCannotBeBothRequiredAndProvided] } }
                    });


            //get skills will be added
            var newOpportunitySkills = request.SkillsIds
                .Where(sId => opportunitySkills.All(os => os.SkillId != sId))
                .Select(sId => skillType == OpportunitySkillType.Provide
                    ? (OpportunitySkill)new OpportunityProvideSkill
                    {
                        SkillId = sId,
                        OpportunityId = request.OpportunityId
                    }
                    : new OpportunityRequireSkill
                    {
                        SkillId = sId,
                        OpportunityId = request.OpportunityId
                    }).ToList();
            //get skills will be deleted
            var toDelete = opportunitySkills
                .Where(os => !request.SkillsIds.Contains(os.SkillId) && os.SkillType == skillType)
                .ToList();
            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _unitOfWork.Repository<OpportunitySkill>().AddRangeAsync(newOpportunitySkills, cancellationToken);
                await _unitOfWork.Repository<OpportunitySkill>().DeleteRangeAsync(toDelete, cancellationToken);

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
