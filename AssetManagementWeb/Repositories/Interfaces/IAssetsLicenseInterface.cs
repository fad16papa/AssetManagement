using System.Threading.Tasks;
using AssetManagementWeb.Models.ApiResponse;
using Domain;

namespace AssetManagementWeb.Repositories.Interfaces
{
    public interface IAssetsLicenseInterface
    {

        Task<object> GetAssetsLicense(string token);
        Task<object> GetAssetsOfLicense(string UserId, string token);
        Task<object> GetLicenseOfAssets(string LicenseId, string token);
        Task<ResponseModel> CreateUserLicense(AssetsLicense assetsLicense, string token);
    }
}