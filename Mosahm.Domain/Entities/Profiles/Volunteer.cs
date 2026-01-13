using Mosahm.Domain.Common.Localization;
using Mosahm.Domain.Entities.Identity;
using Mosahm.Domain.Entities.Location;
using Mosahm.Domain.Entities.Opportunities;
using Mosahm.Domain.Entities.Questions;
using Mosahm.Domain.Enums;

namespace Mosahm.Domain.Entities.Profiles
{
    public class Volunteer : GeneralLocalizableEntity
    {
        #region Properties
        public string NationalId { get; set; }
        public string? ProfileImgUrl { get; set; }
        public string? CoverImgUrl { get; set; }
        public int TotalHours { get; set; }
        public int CompleteOpportunities { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        #endregion
        #region Navigations
        public MosahmUser User { get; set; }
        public Address? Address { get; set; }
        public ICollection<VolunteerSkill> VolunteerSkills { get; set; }
        public ICollection<VolunteerField> VolunteerFields { get; set; }
        public ICollection<OpportunityApplication>? OpportunityApplications { get; set; }
        public ICollection<OpportunitySave>? OpportunitySaves { get; set; }
        public ICollection<OpportunityLike>? OpportunityLikes { get; set; }
        public ICollection<OpportunityComment>? OpportunityComments { get; set; }
        public ICollection<OrganizationFollower>? OrganizationFollwers { get; set; }
        public ICollection<QuestionAnswer>? QuestionAnswers { get; set; }
        #endregion
    }
}
