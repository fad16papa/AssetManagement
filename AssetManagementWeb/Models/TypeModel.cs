using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Models
{
    public enum TypeModel
    {
        [Display(Name = "Laptop")]
        Laptop,
        [Display(Name = "Server")]
        Server,
        [Display(Name = "Monitor")]
        Monitor,
    }
}
