using Mosahm.Domain.Entities.MasterData;

namespace Mosahm.Domain.Entities.Opportunities
{
    public class OpportunityField
    {
        public Guid OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }

        public Guid FieldId { get; set; }
        public Field Field { get; set; }
    }
}
