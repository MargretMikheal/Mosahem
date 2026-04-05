using MediatR;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities;

namespace Mosahem.Application.Features.Volunteers.Commands.FollowOrganization
{
    public class FollowOrganizationCommandHandler : IRequestHandler<FollowOrganizationCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;

        public FollowOrganizationCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
        }

        public async Task<Response<string>> Handle(FollowOrganizationCommand request, CancellationToken cancellationToken)
        {
            var followerRepository = _unitOfWork.Repository<OrganizationFollower>();

            await followerRepository.AddAsync(new OrganizationFollower
            {
                VolunteerId = request.VolunteerId,
                OrganizationId = request.OrganizationId
            }, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Created<string>(null!);
        }
    }
}
