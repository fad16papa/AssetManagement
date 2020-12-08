using AssetManagementWeb.Models.ApiResponse;
using AssetManagementWeb.Models.DTO;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Repositories.Interfaces
{
    public interface IUserStaffInterface
    {
        Task<object> GetUserStaffs(string token);
        Task<object> GetUserStaff(string id, string token);
        Task<ResponseModel> CreateUserStaff(UserStaff userStaff, string token);
        Task<ResponseModel> EditUserStaff(UserStaff userStaff, string token);
        Task<ResponseModel> DeleteUserStaff(UserStaff userStaff, string token);
    }
}
