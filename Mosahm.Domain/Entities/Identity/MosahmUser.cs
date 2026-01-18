using Microsoft.AspNetCore.Identity;
using Mosahm.Domain.Entities.Profiles;
using Mosahm.Domain.Enums;

namespace Mosahm.Domain.Entities.Identity
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