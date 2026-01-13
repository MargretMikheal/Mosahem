using Mosahm.Domain.Common.Localization;

namespace Mosahm.Domain.Entities
{
    public class City : GeneralLocalizableEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public Guid GovernateId { get; set; }
        public Governorate Governorate { get; set; }
        public ICollection<Address>? Addresses { get; set; }
    }
}
