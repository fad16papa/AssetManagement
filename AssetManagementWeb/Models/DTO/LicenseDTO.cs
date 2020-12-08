using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Models.DTO
{
    public class LicenseDTO
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductVersion { get; set; }
        public string LicenseKey { get; set; }
        public bool Expiration { get; set; }
        public DateTime ExpiredOn { get; set; }
        public string Remarks { get; set; }
    }
}
