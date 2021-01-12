using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.AssetLicense;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AssetAPI.Controllers
{
    public class AssetsLicenseController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<AssetsLicense>>> List()
        {
            return await Mediator.Send(new ListAsssetsLicenses.Query());
        }

        [HttpGet, Route("License/{Id}")]
        public async Task<ActionResult<List<AssetsLicense>>> DetailsLicense(Guid Id)
        {
            return await Mediator.Send(new DetailsLicense.Query { LicenseId = Id });
        }

        [HttpGet, Route("Asset/{Id}")]
        public async Task<ActionResult<List<AssetsLicense>>> DetailsAssets(Guid Id)
        {
            return await Mediator.Send(new DetailsAssets.Query { AssetId = Id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateAssetLicense.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}