namespace Mosahem.Application.Features.Volunteers.Commands.EditVolunteerFields
{
    public class EditVolunteerFieldsRequest
    {
        public HashSet<Guid> FieldsIds { get; set; }

        public EditVolunteerFieldsRequest(HashSet<Guid> fieldsIds)
        {
            FieldsIds = fieldsIds;
        }
    }
}
