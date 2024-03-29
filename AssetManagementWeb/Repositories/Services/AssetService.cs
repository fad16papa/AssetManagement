using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.Errors;
using AspNetCore.Http.Extensions;
using AssetManagementWeb.Models.ApiResponse;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using Domain;
using Microsoft.Extensions.Logging;

namespace AssetManagementWeb.Repositories.Services
{
    public class AssetService : IAssetInterface
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AssetService> _logger;
        public AssetService(ILogger<AssetService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> CreateAsset(Asset asset, string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.PostAsJsonAsync<Asset>("api/Assets/", asset);

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
                _logger.LogError($"Error encountered in AssetService||CreateAsset ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public Task<ResponseModel> DeleteAsset(Asset asset, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> EditAsset(Asset asset, string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.PutAsJsonAsync<Asset>("api/Assets/" + asset.Id, asset);

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
                _logger.LogError($"Error encountered in AssetService||EditAsset ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public async Task<object> GetAsset(string id, string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.GetAsync("api/Assets/" + id);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var faliedResponse = await result.Content.ReadAsJsonAsync<RestException>();
                    return new ResponseModel()
                    {
                        ResponseMessage = faliedResponse.Errors.ToString(),
                        ResponseCode = result.StatusCode.ToString()
                    };
                }

                var successResponse = await result.Content.ReadAsJsonAsync<AssetsDTO>();

                return successResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetService||GetAsset ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public async Task<List<AssetsDTO>> GetAssets(string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.GetAsync("api/Assets/");

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.StatusCode.ToString());
                }

                var successResponse = await result.Content.ReadAsJsonAsync<List<AssetsDTO>>();

                return successResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in AssetService||CreateAsset ErrorMessage: {ex.Message}");
                throw ex;
            }
        }
    }
}