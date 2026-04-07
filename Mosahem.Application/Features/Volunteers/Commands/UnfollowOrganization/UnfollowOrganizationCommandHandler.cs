using MediatR;
using Microsoft.EntityFrameworkCore;
using mosahem.Application.Common;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Domain.Entities;

namespace Mosahem.Application.Features.Volunteers.Commands.UnfollowOrganization
{
    public class UnfollowOrganizationCommandHandler : IRequestHandler<UnfollowOrganizationCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseHandler _responseHandler;

        public UnfollowOrganizationCommandHandler(
            IUnitOfWork unitOfWork,
            ResponseHandler responseHandler)
        {
            _unitOfWork = unitOfWork;
            _responseHandler = responseHandler;
        }

        public async Task<Response<string>> Handle(UnfollowOrganizationCommand request, CancellationToken cancellationToken)
        {
            var followerRepository = _unitOfWork.Repository<OrganizationFollower>();

            var relation = await followerRepository.GetTableAsTracking()
                .FirstOrDefaultAsync(f => f.VolunteerId == request.VolunteerId && f.OrganizationId == request.OrganizationId, cancellationToken);

            if (relation is null)
            {
                return _responseHandler.NotFound<string>();
            }

            await followerRepository.DeleteRangeAsync(new[] { relation }, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _responseHandler.Deleted<string>();
        }
    }
}
