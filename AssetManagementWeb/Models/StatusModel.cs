using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Models
{
    public enum StatusModel
    {
        [Display(Name = "Working")]
        Working,
        [Display(Name = "Faulty")]
        Faulty,
        [Display(Name = "Dispose")]
        Dispose,
    }
}
