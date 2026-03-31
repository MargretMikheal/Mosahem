using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Opportunities.Commands.ResumeOpportunity
{
    public class ResumeOpportunityCommandHandler : IRequestHandler<ResumeOpportunityCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ResumeOpportunityCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(ResumeOpportunityCommand request, CancellationToken cancellationToken)
        {
            var opportunity = await _unitOfWork.Opportunities.GetByIdAsync(request.OpportunityId, cancellationToken);

            if (opportunity is null)
                return _responseHandler.NotFound<string>(_localizer[SharedResourcesKeys.Validation.NotFound]);


            opportunity.Status &= ~OpportunityStatus.Stopped;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success(string.Empty, _localizer[SharedResourcesKeys.Success.OpportunityResumed]);
        }
    }
}
