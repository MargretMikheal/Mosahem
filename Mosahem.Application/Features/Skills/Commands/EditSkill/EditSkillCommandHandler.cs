using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Skills.Commands.EditSkill
{
    public class EditSkillCommandHandler : IRequestHandler<EditSkillCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public EditSkillCommandHandler(
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

        public async Task<Response<string>> Handle(EditSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = await _unitOfWork.Skills.GetByIdAsync(request.Id, cancellationToken);
            if (skill is null)
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>>
                    {
                        { "SkillId" ,new List<string> { _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });

            //Mapping the request to the existing skill entity
            request.Adapt(skill);


            await _unitOfWork.Skills.UpdateAsync(skill, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _responseHandler.Success<string>(_localizer[SharedResourcesKeys.General.Updated]);
        }
    }
}
