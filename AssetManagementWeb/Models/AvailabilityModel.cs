using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Models
{
    public enum AvailabilityModel
    {
        [Display(Name = "Yes")]
        Yes,
        [Display(Name = "No")]
        No
    }
}
