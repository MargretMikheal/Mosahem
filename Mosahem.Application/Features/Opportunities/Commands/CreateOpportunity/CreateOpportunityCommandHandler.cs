using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Location;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Entities.Questions;

namespace Mosahem.Application.Features.Opportunities.Commands.CreateOpportunity
{
    public class CreateOpportunityCommandHandler : IRequestHandler<CreateOpportunityCommand, Response<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public CreateOpportunityCommandHandler(
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

        public async Task<Response<Guid>> Handle(CreateOpportunityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transactionStarted = await _unitOfWork.BeginTransactionAsync(cancellationToken);
                if (!transactionStarted)
                {
                    return _responseHandler.BadRequest<Guid>(
                        _localizer[SharedResourcesKeys.General.OperationFailed],
                        BuildError("Database.Transaction", _localizer[SharedResourcesKeys.System.TransactionStartFailed]));
                }

                // 1. Map and Add Opportunity
                var opportunity = _mapper.Map<Opportunity>(request);
                await _unitOfWork.Opportunities.AddAsync(opportunity, cancellationToken);

                // 2. Map and Add Addresses
                if (request.Addresses != null && request.Addresses.Any())
                {
                    var addresses = _mapper.Map<List<Address>>(request.Addresses);
                    addresses.ForEach(a => a.OpportunityId = opportunity.Id);
                    await _unitOfWork.Addresses.AddRangeAsync(addresses, cancellationToken);
                }

                // 3. Map and Add Skills
                var skills = new List<OpportunitySkill>();
                if (request.ProvidedSkillIds != null)
                {
                    var provided = _mapper.Map<List<OpportunityProvideSkill>>(request.ProvidedSkillIds);
                    provided.ForEach(s => s.OpportunityId = opportunity.Id);
                    skills.AddRange(provided);
                }

                if (request.RequiredSkillIds != null)
                {
                    var required = _mapper.Map<List<OpportunityRequireSkill>>(request.RequiredSkillIds);
                    required.ForEach(s => s.OpportunityId = opportunity.Id);
                    skills.AddRange(required);
                }

                if (skills.Any())
                {
                    await _unitOfWork.Repository<OpportunitySkill>().AddRangeAsync(skills, cancellationToken);
                }

                // 4. Map and Add Fields
                if (request.FieldIds != null && request.FieldIds.Any())
                {
                    var fields = _mapper.Map<List<OpportunityField>>(request.FieldIds);
                    fields.ForEach(f => f.OpportunityId = opportunity.Id);
                    await _unitOfWork.Repository<OpportunityField>().AddRangeAsync(fields, cancellationToken);
                }

                if (request.Questions != null && request.Questions.Any())
                {
                    var questions = _mapper.Map<List<Question>>(request.Questions);

                    for (var i = 0; i < questions.Count; i++)
                    {
                        questions[i].OpportunityId = opportunity.Id;
                        questions[i].Order = i + 1;
                    }
                    await _unitOfWork.Questions.AddRangeAsync(questions, cancellationToken);
                }

                var affectedRows = await _unitOfWork.SaveChangesAsync(cancellationToken);
                if (affectedRows <= 0)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                    return _responseHandler.BadRequest<Guid>(
                        _localizer[SharedResourcesKeys.General.OperationFailed],
                        BuildError("Database.SaveChanges", _localizer[SharedResourcesKeys.System.NoRowsAffected]));
                }

                await _unitOfWork.CommitTransactionAsync(cancellationToken);
                return _responseHandler.Created(opportunity.Id);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return _responseHandler.BadRequest<Guid>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    BuildError("Operation", ex.Message));
            }
        }
        private static Dictionary<string, List<string>> BuildError(string key, string message)
        {
            return new Dictionary<string, List<string>>
            {
                { key, new List<string> { message } }
            };
        }
    }
}
