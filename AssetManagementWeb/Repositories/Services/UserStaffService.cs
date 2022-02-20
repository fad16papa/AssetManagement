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
    public class UserStaffService : IUserStaffInterface
    {
        private readonly ILogger<UserStaffService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserStaffService(ILogger<UserStaffService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseModel> CreateUserStaff(UserStaff userStaff, string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.PostAsJsonAsync<UserStaff>("api/UserStaffs/", userStaff);

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
                _logger.LogError($"Error encountered in UserStaffService||CreateUserStaff ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public Task<ResponseModel> DeleteUserStaff(UserStaff userStaff, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> EditUserStaff(UserStaff userStaff, string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.PutAsJsonAsync<UserStaff>("api/UserStaffs/" + userStaff.Id, userStaff);

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
                _logger.LogError($"Error encountered in UserStaffService||EditUserStaff ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public async Task<object> GetUserStaff(string id, string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.GetAsync("api/UserStaffs/" + id);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var faliedResponse = await result.Content.ReadAsJsonAsync<RestException>();
                    return new ResponseModel()
                    {
                        ResponseMessage = faliedResponse.Errors.ToString(),
                        ResponseCode = result.StatusCode.ToString()
                    };
                }

                var successResponse = await result.Content.ReadAsJsonAsync<UserStaffDTO>();

                return successResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in UserStaffService||GetUserStaff ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public async Task<List<UserStaffDTO>> GetUserStaffs(string token)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                responseClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await responseClient.GetAsync("api/UserStaffs/");

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.StatusCode.ToString());
                }

                var successResponse = await result.Content.ReadAsJsonAsync<List<UserStaffDTO>>();

                return successResponse;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in UserStaffService||GetUserStaffs ErrorMessage: {ex.Message}");
                throw ex;
            }
        }
    }
}
