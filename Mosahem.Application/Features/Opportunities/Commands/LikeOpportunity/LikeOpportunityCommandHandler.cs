using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Opportunities;

namespace Mosahem.Application.Features.Opportunities.Commands.LikeOpportunity
{
    public class LikeOpportunityCommandHandler : IRequestHandler<LikeOpportunityCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public LikeOpportunityCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(LikeOpportunityCommand request, CancellationToken cancellationToken)
        {
            var opportunityLike = request.Adapt<OpportunityLike>();

            await _unitOfWork.Repository<OpportunityLike>()
                .AddAsync(opportunityLike, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success(string.Empty, _localizer[SharedResourcesKeys.Success.Added]);
        }
    }
}
