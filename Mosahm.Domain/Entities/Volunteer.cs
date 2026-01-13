using Mosahm.Domain.Common.Localization;
using Mosahm.Domain.Entities.Identity;
using Mosahm.Domain.Enums;

namespace Mosahm.Domain.Entities
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
        public Guid UserId { get; set; }
        public MosahmUser User { get; set; }
        public Address? Address { get; set; }
        public ICollection<VolunteerSkill> VolunteerSkills { get; set; }
        public ICollection<VolunteerField> VolunteerFields { get; set; }
        public ICollection<OpportunityApplication>? VolunteerApplyOpportunities { get; set; }
        public ICollection<OpportunitySave>? VolunteerSaveOpportunities { get; set; }
        public ICollection<OpportunityLike>? VolunteerLikeOpportunities { get; set; }
        public ICollection<OpportunityComment>? VolunteerCommentOpportunities { get; set; }
        public ICollection<OrganizationFollwer>? VolunteerFollowOrganizations { get; set; }
        public ICollection<QuestionAnswer>? QuestionAnswers { get; set; }
        #endregion
    }
}
