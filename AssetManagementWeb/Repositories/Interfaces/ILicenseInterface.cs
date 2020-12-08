using AssetManagementWeb.Models.ApiResponse;
using AssetManagementWeb.Models.DTO;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagementWeb.Repositories.Interfaces
{
    public interface ILicenseInterface
    {
        Task<object> GetLicenses(string token);
        Task<object> GetLicense(string id, string token);
        Task<ResponseModel> CreateLicense(License license, string token);
        Task<ResponseModel> EditLicense(License license, string token);
        Task<ResponseModel> DeleteLicense(License license, string token);
    }
}
