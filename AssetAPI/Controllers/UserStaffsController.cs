using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UserStaffs;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AssetAPI.Controllers
{
    public class UserStaffsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<UserStaff>>> List()
        {
            return await Mediator.Send(new ListUserStaffs.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserStaff>> Details(Guid id)
        {
            return await Mediator.Send(new DetailsUserStaff.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateUserStaff.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}