using Mosahm.Domain.Common.Localization;
using System.Reflection.Metadata.Ecma335;

namespace Mosahm.Domain.Entities.Location
{
    public class Governorate : GeneralLocalizableEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public ICollection<City>? Cities { get; set; }
    }
}
