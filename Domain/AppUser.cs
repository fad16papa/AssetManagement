using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        // public virtual ICollection<Asset> Assets { get; set; }
        // public virtual ICollection<License> Licenses { get; set; }
        public virtual ICollection<AssetHistory> AssetHistories { get; set; }
        public virtual ICollection<LicenseHistory> LicenseHistories { get; set; }
    }
}