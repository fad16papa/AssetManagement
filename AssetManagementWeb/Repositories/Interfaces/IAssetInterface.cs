
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using AssetManagementWeb.Models.ApiResponse;

namespace AssetManagementWeb.Repositories.Interfaces
{
    public interface IAssetInterface
    {
        Task<IEnumerable<Asset>> GetAssets(string token);
        Task<Asset> GetAsset(Guid id, string token);
        Task<ResponseModel> CreateAsset(Asset asset, string token);
        Task<ResponseModel> EditAsset(Asset asset, string token);
        Task<ResponseModel> DeleteAsset(Asset asset, string token);
    }
}