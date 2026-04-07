using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Volunteers.Commands.EditVolunteerFields
{
    public class EditVolunteerFieldsCommand : IRequest<Response<string>>
    {
        public EditVolunteerFieldsCommand(Guid volunteerId, HashSet<Guid> fieldIds)
        {
            VolunteerId = volunteerId;
            FieldIds = fieldIds;
        }

        public Guid VolunteerId { get; set; }
        public HashSet<Guid> FieldIds { get; set; }
    }
}
