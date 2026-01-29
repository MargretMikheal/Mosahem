using Mosahem.Domain.Entities;
using System.Globalization;

namespace mosahem.Domain.Common.Localization
{
    public abstract class GeneralLocalizableEntity : BaseEntity
    {
        protected string Localize(string ar, string en)
        {
            var culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower();
            return culture == "ar" ? ar : en;
        }
    }
}
