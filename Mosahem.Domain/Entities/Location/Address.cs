using mosahem.Domain.Entities.Opportunities;
using mosahem.Domain.Entities.Profiles;
using Mosahem.Domain.Entities;
using System.Reflection.Emit;

namespace mosahem.Domain.Entities.Location
{
    public class Address : BaseEntity
    {
        public string? Description { get; set; }
        #region Navigations
        public Guid CityId { get; set; }
        public City City { get; set; }
        public Guid? VolunteerId { get; set; }
        public Volunteer? Volunteer { get; set; }
        public Guid? OpportunityId { get; set; }
        public Opportunity? Opportunity { get; set; }
        public Guid? OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        #endregion
    }
}
