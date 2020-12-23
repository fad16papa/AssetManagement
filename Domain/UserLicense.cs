using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class UserLicense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid LicenseId { get; set; }
        public virtual License License { get; set; }
        public Guid UserStaffId { get; set; }
        public virtual UserStaff UserStaff { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
        public string IsActive { get; set; }
    }
}