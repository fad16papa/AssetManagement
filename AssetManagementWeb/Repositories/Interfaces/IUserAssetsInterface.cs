using System.Threading.Tasks;
using AssetManagementWeb.Models.ApiResponse;
using Domain;

namespace AssetManagementWeb.Repositories.Interfaces
{
    public interface IUserAssetsInterface
    {
        Task<object> GetUserAssets(string token);
        Task<object> GetUserOfAssets(string Id, string token);
        Task<object> GetAssetsOfUser(string Id, string token);
        Task<ResponseModel> CreateUserAssets(UserAssets userAssets, string token);
    }
}