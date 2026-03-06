using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Skills.Queries.GetAllSkills
{
    public class GetAllSkillsQuery : IRequest<Response<IReadOnlyList<GetAllSkillsQueryResponse>>>
    {
    }
}
