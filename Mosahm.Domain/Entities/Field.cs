using Mosahm.Domain.Common.Localization;

namespace Mosahm.Domain.Entities
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
