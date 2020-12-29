using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementWeb.Components
{
    public class UserStaffsComponents : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly IUserStaffInterface _userStaffInterface;
        public UserStaffsComponents(IUserStaffInterface userStaffInterface, IMapper mapper)
        {
            _userStaffInterface = userStaffInterface;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _userStaffInterface.GetUserStaffs(Request.Cookies["AssetReference"].ToString());

            //Map the objects results to corresponding DTO's
            IEnumerable<UserStaffDTO> userStaffDTOs = _mapper.Map<IEnumerable<UserStaffDTO>>(result);

            ViewBag.TotaUserStaffCount = userStaffDTOs.ToList().Count();

            return View(userStaffDTOs);
        }
    }
}