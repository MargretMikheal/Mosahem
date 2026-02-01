using mosahem.Domain.Common.Localization;
using mosahem.Domain.Entities.Identity;
using mosahem.Domain.Entities.Location;
using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Enums;

namespace mosahem.Domain.Entities.Profiles
{
    public class Organization : GeneralLocalizableEntity
    {
        #region Properties
        public string Description { get; set; }
        public string? LogoKey { get; set; }
        public string? LicenseKey { get; set; }
        public string? VerificationComment { get; set; }
        public VerficationStatus VerificationStatus { get; set; }
        #endregion
        #region Navigations
        public MosahmUser User { get; set; }
        public ICollection<Address> Address { get; set; }
        public ICollection<OrganizationFollower>? OrganizationFollwers { get; set; }
        public ICollection<OrganizationField> OrganizationFields { get; set; }
        public ICollection<Opportunity>? Opportunities { get; set; }
        #endregion

    }
}
