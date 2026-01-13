using Microsoft.AspNetCore.Identity;
using Mosahm.Domain.Enums;

namespace Mosahm.Identity.Models
{
    public class MosahmUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public AuthProvider AuthProvider { get; set; }
        public bool IsDeleted { get; set; }
    }
}
