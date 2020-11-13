using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Assets;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetAPI.Controllers
{
    public class AssetsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Asset>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}