using Mapster;
using MediatR;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using Mosahem.Application.Interfaces;

namespace Mosahem.Application.Features.Volunteers.Queries.GetAllVolunteers
{
    public class GetAllVolunteersQueryHandler : IRequestHandler<GetAllVolunteersQuery, Response<IReadOnlyList<GetAllVolunteersQueryResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;
        private readonly IFileService _fileService;
        public GetAllVolunteersQueryHandler(IUnitOfWork unitOfWork, ResponseHandler responseHandler, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
            _fileService = fileService;
        }

        public async Task<Response<IReadOnlyList<GetAllVolunteersQueryResponse>>> Handle(GetAllVolunteersQuery request, CancellationToken cancellationToken)
        {
            var volunteers = await _unitOfWork.Volunteers.GetVolunteersWithProfilesAsync(cancellationToken);

            var response = volunteers.Adapt<IReadOnlyList<GetAllVolunteersQueryResponse>>();
            foreach (var volunteer in response)
                volunteer.ProfileImage = _fileService.GetFileUrl(volunteer.ProfileImage, isPrivate: true);

            return _responseHandler.Success<IReadOnlyList<GetAllVolunteersQueryResponse>>(response);
        }
    }
}
