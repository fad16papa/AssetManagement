using System;

namespace AssetManagementWeb.Models.DTO
{
    public class UserLicenseDTO
    {
        public Guid Id { get; set; }
        public Guid LicenseId { get; set; }
        public virtual LicenseDTO LicenseDTO { get; set; }
        public Guid UserStaffId { get; set; }
        public virtual UserStaffDTO UserStaffDTO { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
    }
}