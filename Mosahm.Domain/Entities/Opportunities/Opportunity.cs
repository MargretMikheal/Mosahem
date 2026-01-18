using Mosahm.Domain.Common.Localization;
using Mosahm.Domain.Entities.Location;
using Mosahm.Domain.Entities.Profiles;
using Mosahm.Domain.Entities.Questions;
using Mosahm.Domain.Enums;

namespace Mosahm.Domain.Entities.Opportunities
{
    public class Opportunity : BaseEntity
    {
        #region Properties
        public string Title { get; set; }
        public string Descripition { get; set; }
        public string LogoUrl { get; set; }
        public OpportunityStatus Status { get; set; }
        public VerficationStatus VerificationStatus { get; set; }
        public OpportunityWorkType WorkType { get; set; }
        public OpportunityLocationType LocationType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Vacancies { get; set; }
        public int ApplicantsCount { get; set; }
        #endregion
        #region Navigations
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public ICollection<Address>? Address { get; set; }
        public ICollection<OpportunityComment>? OpportunityComments { get; set; }
        public ICollection<OpportunityLike>? OpportunityLikes { get; set; }
        public ICollection<OpportunitySave>? OpportunitySaves { get; set; }
        public ICollection<OpportunityApplication>? OpportunityApplications { get; set; }
        public ICollection<OpportunitySkill>? OpportunitySkills { get; set; }
        public ICollection<Question>? Questions { get; set; }
        public ICollection<OpportunityField>? OpportunityFields { get; set; }
        #endregion
    }
}
