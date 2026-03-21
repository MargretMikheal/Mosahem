namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunityFields
{
    public class EditOpportunityFieldsRequest
    {
        public HashSet<Guid> FieldsIds { get; set; }

        public EditOpportunityFieldsRequest(HashSet<Guid> fieldsIds)
        {
            FieldsIds = fieldsIds;
        }
    }
}
