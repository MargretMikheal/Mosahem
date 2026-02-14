using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.MasterData;

namespace Mosahem.Application.Features.Skills.Commands.AddSkill
{
    public class AddSkillCommandHandler : IRequestHandler<AddSkillCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public AddSkillCommandHandler(
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

        public async Task<Response<string>> Handle(AddSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = _mapper.Map<Skill>(request);
            await _unitOfWork.Skills.AddAsync(skill, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Created<string>(_localizer[SharedResourcesKeys.General.Created]);
        }
    }
}
