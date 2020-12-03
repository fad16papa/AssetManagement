using AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AssetManagementWeb.Helper;
using AssetManagementWeb.Repositories.Interfaces;
using AssetManagementWeb.Models.ApiResponse;
using Application.Errors;
using System.Net;
using Application.User;
using Domain;
using System.Net.Http.Headers;
using AssetManagementWeb.Models.DTO;

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

        public async Task<object> CurrentUser()
        {
            try
            {
                var responseClient = _httpClientFactory.CreateClient("AssetAPI");

                var result = await responseClient.GetAsync("api/Users");

                if (result.StatusCode != HttpStatusCode.OK)
                {
                    var faliedResponse = await result.Content.ReadAsJsonAsync<RestException>();
                    return new ResponseModel()
                    {
                        ResponseMessage = faliedResponse.Errors.ToString(),
                        ResponseCode = result.StatusCode.ToString()
                    };
                }

                var successResponse = await result.Content.ReadAsJsonAsync<User>();
                return new UserDTO()
                {
                    DisplayName = successResponse.DisplayName,
                    UserName = successResponse.UserName,
                    Email = successResponse.Email,
                    Token = successResponse.Token,
                    Id = successResponse.Id
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error encountered in UserService||CurrentUser ErrorMessage: {ex.Message}");
                throw ex;
            }
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