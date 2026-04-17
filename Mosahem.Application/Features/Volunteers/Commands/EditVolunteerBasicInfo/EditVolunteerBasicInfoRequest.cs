using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Volunteers.Commands.EditVolunteerBasicInfo
{
    public class EditVolunteerBasicInfoRequest
    {
        public string? NationalId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? Bio { get; set; }
    }
}
