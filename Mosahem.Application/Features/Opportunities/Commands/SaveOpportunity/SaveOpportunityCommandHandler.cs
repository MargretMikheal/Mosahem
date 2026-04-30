using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Opportunities;

namespace Mosahem.Application.Features.Opportunities.Commands.SaveOpportunity
{
    public class SaveOpportunityCommandHandler : IRequestHandler<SaveOpportunityCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public SaveOpportunityCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(SaveOpportunityCommand request, CancellationToken cancellationToken)
        {
            var opportunitySave = request.Adapt<OpportunitySave>();

            await _unitOfWork.Repository<OpportunitySave>()
                .AddAsync(opportunitySave, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Success(string.Empty, _localizer[SharedResourcesKeys.Success.Added]);
        }
    }
}
