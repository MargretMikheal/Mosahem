using mosahem.Domain.Common.Localization;
using mosahem.Domain.Entities.Opportunities;

namespace mosahem.Domain.Entities.MasterData
{
    public class Field : GeneralLocalizableEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        #region Navigations
        public ICollection<VolunteerField>? VolunteerFields { get; set; }
        public ICollection<OrganizationField>? OrganizationFields { get; set; }
        public ICollection<OpportunityField>? OpportunityFields { get; set; }
        #endregion
    }
}
