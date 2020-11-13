using System;

namespace Domain
{
    public class LicenseHistory
    {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public Guid LicenseId { get; set; }
        public virtual License License { get; set; }
    }
}