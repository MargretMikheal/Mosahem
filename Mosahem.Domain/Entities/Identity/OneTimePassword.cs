using mosahem.Domain.Entities.Identity;
using Mosahem.Domain.Enums;

namespace Mosahem.Domain.Entities.Identity
{
    public class OneTimePassword : BaseEntity
    {
        public string Code { get; set; } 
        public OtpPurpose Purpose { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }

        public string Email { get; set; }

        public Guid? UserId { get; set; } 
        public virtual MosahmUser? User { get; set; }
        public bool IsValid()
        {
            return !IsUsed && DateTime.UtcNow <= ExpiresAt;
        }
    }
}