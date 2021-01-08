using System.Net.Http;
using System.Threading.Tasks;
using AssetManagementWeb.Models.ApiResponse;
using AssetManagementWeb.Repositories.Interfaces;
using Domain;
using Microsoft.Extensions.Logging;

namespace AssetManagementWeb.Repositories.Services
{
    public class AssetsLicenseService : IAssetsLicenseInterface
    {
        private readonly ILogger<AssetsLicenseService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public AssetsLicenseService(ILogger<AssetsLicenseService> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public Task<ResponseModel> CreateUserLicense(AssetsLicense assetsLicense, string token)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> GetAssetsLicense(string token)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> GetAssetsOfLicense(string UserId, string token)
        {
            throw new System.NotImplementedException();
        }

        public Task<object> GetLicenseOfAssets(string LicenseId, string token)
        {
            throw new System.NotImplementedException();
        }
    }
}