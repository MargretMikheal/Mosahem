using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Volunteers.Queries.GetVolunteerProfile
{
    public class GetVolunteerProfileQuery : IRequest<Response<GetVolunteerProfileResponse>>
    {
        public Guid VolunteerId { get; set; }
    }
}
