using Mosahm.Domain.Common.Localization;

namespace Mosahm.Domain.Entities
{
    public class Skill : GeneralLocalizableEntity
    {
        public string NameAr {  get; set; }
        public string NameEn { get; set; }
        public string Category { get; set; }
        #region Navigations
        public ICollection<VolunteerSkill>? VolunteerSkills { get; set; }
        public ICollection<OpportunitySkill>? OpportunitySkills { get; set; }
        #endregion
    }
}