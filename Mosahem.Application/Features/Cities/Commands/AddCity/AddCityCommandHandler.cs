using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;
using mosahem.Domain.Entities.Location;

namespace Mosahem.Application.Features.Cities.Commands.AddCity
{
    public class AddCityCommandHandler : IRequestHandler<AddCityCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMapper _mapper;

        public AddCityCommandHandler(
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

        public async Task<Response<string>> Handle(AddCityCommand request, CancellationToken cancellationToken)
        {
            //Check if the governorate exists
            var governate = await _unitOfWork.Governorates.GetByIdAsync(request.GovernorateId, cancellationToken);
            if (governate is null)
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>>
                    {
                        { $"{nameof(request.GovernorateId)}", new List<string> { _localizer[SharedResourcesKeys.Validation.NotFound] } }
                    });

            var city = _mapper.Map<City>(request);

            try
            {
                await _unitOfWork.Cities.AddAsync(city, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<string>(
                    _localizer[SharedResourcesKeys.General.OperationFailed],
                    new Dictionary<string, List<string>>
                    {
                        { "Exception", new List<string> { ex.Message } }
                    });
            }

            return _responseHandler.Created<string>(_localizer[SharedResourcesKeys.General.Created]);
        }
    }
}
