using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.Errors;
using Application.User;
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

        public async Task<LoginResponeModel> Login(LoginDTO loginDTO)
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                var result = await responseClient.PostAsJsonAsync<LoginDTO>("api/Users/login", loginDTO);

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var faliedResponse = await result.Content.ReadAsJsonAsync<RestException>();
                    return new LoginResponeModel()
                    {
                        Message = faliedResponse.Errors.ToString(),
                        Code = Convert.ToInt32(result.StatusCode)
                    };
                }

                var successResponse = await result.Content.ReadAsJsonAsync<User>();
                return new LoginResponeModel()
                {
                    DisplayName = successResponse.DisplayName,
                    UserName = successResponse.UserName,
                    Token = successResponse.Token,
                    Code = Convert.ToInt32(result.StatusCode)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in UserService||LoginUser ErrorMessage: {ex.Message}");
                throw ex;
            }
        }

        public Task<object> Register(RegsiterDTO regsiterDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}