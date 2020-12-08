using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Licenses;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AssetAPI.Controllers
{
    public class LicenseController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<License>>> List()
        {
            return await Mediator.Send(new ListLicense.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<License>> Details(Guid id)
        {
            return await Mediator.Send(new DetailsLicense.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateLicense.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, EditLicense.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }
    }
}