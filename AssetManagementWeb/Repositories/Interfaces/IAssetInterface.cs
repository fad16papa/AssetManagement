
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using AssetManagementWeb.Models.ApiResponse;
using AssetManagementWeb.Models.DTO;
using System.Linq;

namespace AssetManagementWeb.Repositories.Interfaces
{
    public interface IAssetInterface
    {
        Task<List<AssetsDTO>> GetAssets(string token);
        Task<object> GetAsset(string id, string token);
        Task<ResponseModel> CreateAsset(Asset asset, string token);
        Task<ResponseModel> EditAsset(Asset asset, string token);
        Task<ResponseModel> DeleteAsset(Asset asset, string token);
    }
}