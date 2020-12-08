using System.Threading.Tasks;
using AssetManagementWeb.Models.ApiResponse;
using AssetManagementWeb.Models.DTO;

namespace AssetManagementWeb.Repositories.Interfaces
{
    public interface IUserInterface
    {
        Task<LoginResponeModel> Login(LoginDTO loginDTO);
        Task<object> Register(RegsiterDTO regsiterDTO);
        Task<object> CurrentUser(string token);
    }
}