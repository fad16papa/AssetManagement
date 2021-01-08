using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AssetsLicense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid AssetsId { get; set; }
        public virtual Asset Asset { get; set; }
        public Guid LicenseId { get; set; }
        public virtual License License { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
        public string IsActive { get; set; }
    }
}