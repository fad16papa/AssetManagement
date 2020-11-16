using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class UserAssets
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
        public DateTime DateCreated { get; set; }
    }
}