using System.Threading.Tasks;
using AssetManagementWeb.Models.ApiResponse;
using Domain;

namespace AssetManagementWeb.Repositories.Interfaces
{
    public interface IUserLicenseInterface
    {
        Task<object> GetUserLicense(string token);
        Task<object> GetUserOfLicense(string UserId, string token);
        Task<object> GetLicensesOfUser(string LicenseId, string token);
        Task<ResponseModel> CreateUserLicense(UserLicense userLicense, string token);
        Task<ResponseModel> EditUserLicense(UserLicense userLicense, string token);
    }
}