using Application.Errors;
using AspNetCore.Http.Extensions;
using AssetManagementWeb.Models.ApiResponse;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AssetManagementWeb.Repositories.Services
{
    public class LicenseService : ILicenseInterface
    {
        private readonly ILogger<LicenseService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public LicenseService(ILogger<LicenseService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> CreateLicense(License license, string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.PostAsJsonAsync<License>("api/License/", license);

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
                _logger.LogError($"Error encountered in LicenseService||CreateLicense ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public Task<ResponseModel> DeleteLicense(License license, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> EditLicense(License license, string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.PutAsJsonAsync<License>("api/License/" + license.Id, license);

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
                _logger.LogError($"Error encountered in LicenseService||EditLicense ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public async Task<object> GetLicense(string id, string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.GetAsync("api/License/" + id);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var faliedResponse = await result.Content.ReadAsJsonAsync<RestException>();
                    return new ResponseModel()
                    {
                        ResponseMessage = faliedResponse.Errors.ToString(),
                        ResponseCode = result.StatusCode.ToString()
                    };
                }

                var successResponse = await result.Content.ReadAsJsonAsync<LicenseDTO>();

                return successResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in LicenseService||GetLicense ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public async Task<object> GetLicenses(string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.GetAsync("api/License/");

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var faliedResponse = await result.Content.ReadAsJsonAsync<RestException>();
                    return new ResponseModel()
                    {
                        ResponseMessage = faliedResponse.Errors.ToString(),
                        ResponseCode = result.StatusCode.ToString()
                    };
                }

                var successResponse = await result.Content.ReadAsJsonAsync<List<LicenseDTO>>();

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
