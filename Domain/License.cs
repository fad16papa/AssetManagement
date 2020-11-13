using System;
using System.Collections.Generic;

namespace Domain
{
    public class License
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductVersion { get; set; }
        public string LicenseKey { get; set; }
        public bool Expiration { get; set; }
        public DateTime ExpiredOn { get; set; }
        public string Remarks { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}