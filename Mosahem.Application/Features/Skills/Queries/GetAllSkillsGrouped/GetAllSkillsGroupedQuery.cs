using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Skills.Queries.GetAllSkillsGrouped
{
    public class GetAllSkillsGroupedQuery : IRequest<Response<IReadOnlyList<GetAllSkillsGroupedQueryResponse>>>
    {
        public IReadOnlyList<Guid>? FieldIds { get; set; }
    }
}
