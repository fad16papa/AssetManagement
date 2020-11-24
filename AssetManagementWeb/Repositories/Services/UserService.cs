using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.Errors;
using AspNetCore.Http.Extensions;
using AssetManagementWeb.Models.ApiResponse;
using AssetManagementWeb.Models.DTO;
using AssetManagementWeb.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace AssetManagementWeb.Repositories.Services
{
    public class UserService : IUserInterface
    {
        private readonly ILogger<UserService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public UserService(ILogger<UserService> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<object> Login(LoginDTO loginDTO)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                var result = await responseClient.PostAsJsonAsync<LoginDTO>("api/Users/Login", loginDTO);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var faliedResponse = await result.Content.ReadAsJsonAsync<RestException>();
                    return new ResponseModel()
                    {
                        ResponseMessage = faliedResponse.Errors.ToString(),
                        ResponseCode = result.StatusCode.ToString()
                    };
                }

                var successResponse = await result.Content.ReadAsJsonAsync<LoginResponeModel>();

                return new LoginResponeModel()
                {
                    DisplayName = successResponse.DisplayName,
                    Token = successResponse.Token,
                    UserName = successResponse.UserName,
                    ResponseCode = result.StatusCode.ToString()
                };

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in UserService||Login ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public Task<object> Register(RegsiterDTO regsiterDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}