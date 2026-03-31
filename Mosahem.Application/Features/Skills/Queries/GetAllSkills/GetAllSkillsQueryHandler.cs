using MapsterMapper;
using MediatR;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;

namespace Mosahem.Application.Features.Skills.Queries.GetAllSkills
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, Response<IReadOnlyList<GetAllSkillsQueryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IMapper _mapper;

        public GetAllSkillsQueryHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<GetAllSkillsQueryResponse>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await _unitOfWork.Skills.GetAllForListingAsync(cancellationToken);

            var response = _mapper.Map<IReadOnlyList<GetAllSkillsQueryResponse>>(skills);

            return _responseHandler.Success<IReadOnlyList<GetAllSkillsQueryResponse>>(response);
        }
    }
}
