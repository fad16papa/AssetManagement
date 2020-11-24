using System.Threading.Tasks;
using AssetManagementWeb.Models.DTO;

namespace AssetManagementWeb.Repositories.Interfaces
{
    public interface IUserInterface
    {
        Task<object> Login(LoginDTO loginDTO);
        Task<object> Register(RegsiterDTO regsiterDTO);
    }
}