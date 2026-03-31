using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using static mosahem.Application.Resources.SharedResourcesKeys;

namespace Mosahem.Application.Features.Skills.Queries.GetAllSkillsGrouped
{
    public class GetAllSkillsGroupedQueryHandler : IRequestHandler<GetAllSkillsGroupedQuery, Response<IReadOnlyList<GetAllSkillsGroupedQueryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public GetAllSkillsGroupedQueryHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResources> localizer,

            ResponseHandler responseHandler,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<GetAllSkillsGroupedQueryResponse>>> Handle(GetAllSkillsGroupedQuery request, CancellationToken cancellationToken)
        {
            var groupedSkills = await _unitOfWork.Skills.GetAllGroupedAsync(request.FieldIds, cancellationToken);
            if (!groupedSkills.Any())
                return _responseHandler.NotFound<IReadOnlyList<GetAllSkillsGroupedQueryResponse>>(_localizer[User.NotFound]);

            return _responseHandler.Success<IReadOnlyList<GetAllSkillsGroupedQueryResponse>>(groupedSkills);
        }


    }
}
