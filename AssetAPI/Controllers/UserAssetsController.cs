using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UserAsset;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AssetAPI.Controllers
{
    public class UserAssetsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<UserAssets>>> List()
        {
            return await Mediator.Send(new ListUserAssets.Query());
        }

        [HttpGet, Route("User/{Id}")]
        public async Task<ActionResult<List<UserAssets>>> DetailsUser(Guid Id)
        {
            return await Mediator.Send(new DetailsUser.Query { UserId = Id });
        }

        [HttpGet, Route("Asset/{Id}")]
        public async Task<ActionResult<List<UserAssets>>> DetailsAssets(Guid Id)
        {
            return await Mediator.Send(new DetailsAssets.Query { AssetId = Id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateUserAssets.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}