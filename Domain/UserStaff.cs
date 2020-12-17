using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain
{
    public class UserStaff
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual IList<UserAssets> UserAssets { get; set; }
    }
}