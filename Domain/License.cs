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
        public string Expiration { get; set; }
        public DateTime ExpiredOn { get; set; }
        public string IsAvailable { get; set; }
        public string Remarks { get; set; }
    }
}