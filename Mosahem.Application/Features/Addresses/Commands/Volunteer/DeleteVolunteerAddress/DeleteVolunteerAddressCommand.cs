using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Volunteers.Commands.DeleteVolunteerAddress
{
    public class DeleteVolunteerAddressCommand : IRequest<Response<string>>
    {
        public Guid VolunteerId { get; set; }

        public DeleteVolunteerAddressCommand(Guid volunteerId)
        {
            VolunteerId = volunteerId;
        }
    }
}
