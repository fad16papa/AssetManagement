using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class HistoryUserAssets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid AssetsId { get; set; }
        public virtual Asset Asset { get; set; }
        public Guid UserStaffId { get; set; }
        public virtual UserStaff UserStaff { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
        public string IsActive { get; set; }
    }
}