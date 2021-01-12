using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.Errors;
using AspNetCore.Http.Extensions;
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

        public async Task<ResponseModel> CreateAssetsLicense(AssetsLicense assetsLicense, string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.PostAsJsonAsync<AssetsLicense>("api/AssetsLicense", assetsLicense);

                result.Content.ReadAsStringAsync().ToString();

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var faliedResponse = await result.Content.ReadAsJsonAsync<RestException>();
                    return new ResponseModel()
                    {
                        ResponseMessage = faliedResponse.Errors.ToString(),
                        ResponseCode = result.StatusCode.ToString()
                    };
                }

                return new ResponseModel()
                {
                    ResponseCode = result.StatusCode.ToString()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsLicenseService||CreateAssetsLicense ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public async Task<object> GetAssetsLicense(string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.GetAsync("api/AssetsLicense/");

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var faliedResponse = await result.Content.ReadAsJsonAsync<RestException>();
                    return new ResponseModel()
                    {
                        ResponseMessage = faliedResponse.Errors.ToString(),
                        ResponseCode = result.StatusCode.ToString()
                    };
                }

                var successResponse = await result.Content.ReadAsJsonAsync<List<AssetsLicense>>();

                return successResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsLicenseService||GetAssetsLicense ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public async Task<object> GetAssetsOfLicense(string AssetId, string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.GetAsync("api/AssetsLicense/Asset/" + AssetId);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var faliedResponse = await result.Content.ReadAsJsonAsync<RestException>();
                    return new ResponseModel()
                    {
                        ResponseMessage = faliedResponse.Errors.ToString(),
                        ResponseCode = result.StatusCode.ToString()
                    };
                }

                var successResponse = await result.Content.ReadAsJsonAsync<List<AssetsLicense>>();

                return successResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsLicenseService||GetAssetsOfLicense ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public async Task<object> GetLicenseOfAssets(string LicenseId, string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.GetAsync("api/AssetsLicense/License/" + LicenseId);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var faliedResponse = await result.Content.ReadAsJsonAsync<RestException>();
                    return new ResponseModel()
                    {
                        ResponseMessage = faliedResponse.Errors.ToString(),
                        ResponseCode = result.StatusCode.ToString()
                    };
                }

                var successResponse = await result.Content.ReadAsJsonAsync<List<AssetsLicense>>();

                return successResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetsLicenseService||GetLicenseOfAssets ErrorMessage: {ex.Message}");
                throw ex;
            }
        }
    }
}