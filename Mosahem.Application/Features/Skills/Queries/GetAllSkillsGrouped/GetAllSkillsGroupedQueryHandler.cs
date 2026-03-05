using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.MasterData;
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
            var skills = await _unitOfWork.Skills.GetAllWithFieldAsync(cancellationToken);
            if (!skills.Any())
                return _responseHandler.NotFound<IReadOnlyList<GetAllSkillsGroupedQueryResponse>>(_localizer[User.NotFound]);

            var fieldOrder = BuildFieldOrder(request.FieldIds);

            var groupedSkills = skills
                .Where(skill => skill.Field is not null)
                .GroupBy(skill => new
                {
                    skill.FieldId,
                    CategoryName = skill.Field.Localize(skill.Field.NameAr, skill.Field.NameEn)
                })
                .Select(group => new GetAllSkillsGroupedQueryResponse
                {
                    FieldId = group.Key.FieldId,
                    Category = group.Key.CategoryName,
                    Skills = MapSkills(group.ToList(), _mapper)
                })
                .OrderBy(group => GetOrder(group.FieldId, fieldOrder))
                .ThenBy(group => group.Category)
                .ToList();

            return _responseHandler.Success<IReadOnlyList<GetAllSkillsGroupedQueryResponse>>(groupedSkills);
        }

        private static Dictionary<Guid, int> BuildFieldOrder(IReadOnlyList<Guid>? fieldIds)
        {
            var order = new Dictionary<Guid, int>();
            if (fieldIds is null)
                return order;

            var index = 0;
            foreach (var fieldId in fieldIds)
            {
                if (order.ContainsKey(fieldId))
                    continue;

                order[fieldId] = index;
                index++;
            }

            return order;
        }

        private static int GetOrder(Guid fieldId, IReadOnlyDictionary<Guid, int> fieldOrder)
        {
            return fieldOrder.TryGetValue(fieldId, out var order) ? order : int.MaxValue;
        }

        private static IReadOnlyList<GetAllSkillsGroupedSkillResponse> MapSkills(IReadOnlyList<Skill> skills, IMapper mapper)
        {
            return mapper.Map<IReadOnlyList<GetAllSkillsGroupedSkillResponse>>(skills)
                .OrderBy(skill => skill.Name)
                .ToList();
        }
    }
}
