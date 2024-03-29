using System.Threading.Tasks;
using AssetManagementWeb.Models.ApiResponse;
using AssetManagementWeb.Models.DTO;
using Domain;

namespace AssetManagementWeb.Repositories.Interfaces
{
    public interface IUserAssetsInterface
    {
        Task<object> GetUserAssets(string token);
        Task<object> GetUserOfAssets(string UserId, string token);
        Task<object> GetAssetsOfUser(string AssetId, string token);
        Task<ResponseModel> CreateUserAssets(UserAssets userAssets, string token);
        Task<ResponseModel> EditUserAssets(UserAssets userAssets, string token);
    }
}