using Microsoft.AspNetCore.Identity;
using mosahem.Domain.Entities.Profiles;
using mosahem.Domain.Enums;

namespace mosahem.Domain.Entities.Identity
{
    public class MosahmUser : IdentityUser<Guid>
    {
        #region Properties
        public string FullName { get; set; }
        public UserRole Role { get; set; }
        public AuthProvider AuthProvider { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool IsDeleted { get; set; }
        #endregion

        #region Navigations
        public Volunteer? Volunteer { get; set; }
        public Organization? Organization { get; set; }
        #endregion
    }
}