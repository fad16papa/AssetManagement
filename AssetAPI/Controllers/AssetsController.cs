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

        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> Details(Guid id)
        {
            return await Mediator.Send(new DetailsAsset.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateAsset.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Unit>> Edit(Guid id, EditAsset.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }
    }
}