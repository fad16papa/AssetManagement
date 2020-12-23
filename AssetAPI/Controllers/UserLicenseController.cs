using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UserLicenses;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AssetAPI.Controllers
{
    public class UserLicenseController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<UserLicense>>> List()
        {
            return await Mediator.Send(new ListUserLicense.Query());
        }

        [HttpGet, Route("User/{Id}")]
        public async Task<ActionResult<List<UserLicense>>> DetailsUser(Guid Id)
        {
            return await Mediator.Send(new DetailsUser.Query { UserId = Id });
        }

        [HttpGet, Route("License/{Id}")]
        public async Task<ActionResult<List<UserLicense>>> DetailsLicense(Guid Id)
        {
            return await Mediator.Send(new DetailsLicense.Query { LicenseId = Id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateUserLicense.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}