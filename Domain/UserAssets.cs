using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain
{
    public class UserAssets
    {
        public Guid Id { get; set; }
        public Guid AssetsId { get; set; }
        public virtual Asset Asset { get; set; }
        public Guid UserStaffId { get; set; }
        public virtual UserStaff UserStaff { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
    }
}