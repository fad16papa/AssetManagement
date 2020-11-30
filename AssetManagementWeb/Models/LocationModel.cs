using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Models
{
    public enum LocationModel
    {
        [Display(Name = "IT")]
        IT,
        [Display(Name = "Accounting")]
        Accounting,
        [Display(Name = "Site")]
        Site,
        [Display(Name = "Warehouse")]
        Warehouse,
        [Display(Name = "Server Room")]
        ServerRoom,
        [Display(Name = "Tuas")]
        Tuas,
        [Display(Name = "First Level")]
        FirstLevel,
        [Display(Name = "Second Level")]
        SecondLevel,
        [Display(Name = "Third Level")]
        ThirdLevel,
        [Display(Name = "Fourth Level")]
        FourthLevel
    }
}
