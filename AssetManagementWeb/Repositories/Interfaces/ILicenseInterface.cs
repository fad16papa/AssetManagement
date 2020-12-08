using AssetManagementWeb.Models.ApiResponse;
using AssetManagementWeb.Models.DTO;
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
        Task<ResponseModel> CreateLicense(LicenseDTO licenseDTO, string token);
        Task<ResponseModel> EditLicense(LicenseDTO licenseDTO, string token);
        Task<ResponseModel> DeleteLicense(LicenseDTO licenseDTO, string token);
    }
}
