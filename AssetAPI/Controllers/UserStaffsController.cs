using System.Threading.Tasks;
using Application.UserStaffs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AssetAPI.Controllers
{
    public class UserStaffsController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateUserStaff.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}