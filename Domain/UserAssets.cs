using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain
{
    public class UserAssets
    {
        public Guid AssetsId { get; set; }
        public virtual Asset Asset { get; set; }
        public virtual Guid UserStaffId { get; set; }
        public UserStaff UserStaff { get; set; }
    }
}