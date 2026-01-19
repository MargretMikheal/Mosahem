using mosahem.Domain.Common.Localization;

namespace mosahem.Domain.Entities.Location
{
    public class City : GeneralLocalizableEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public Guid GovernorateId { get; set; }
        public Governorate Governorate { get; set; }
        public ICollection<Address>? Addresses { get; set; }
    }
}
