using MediatR;
using mosahem.Application.Common;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Volunteers.Commands.EditVolunteerBasicInfoCommand
{
    public class EditVolunteerBasicInfoCommand : IRequest<Response<string>>
    {
        public Guid VolunteerId { get; set; }
        public string? NationalId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? Bio { get; set; }
    }
}
