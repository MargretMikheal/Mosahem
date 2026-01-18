using Mosahm.Domain.Common.Localization;
using Mosahm.Domain.Entities.Identity;
using Mosahm.Domain.Entities.Location;
using Mosahm.Domain.Entities.Opportunities;
using Mosahm.Domain.Enums;

namespace Mosahm.Domain.Entities.Profiles
{
    public class Organization : GeneralLocalizableEntity
    {
        #region Properties
        public string Description { get; set; }
        public string LicenseUrl { get; set; }
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
