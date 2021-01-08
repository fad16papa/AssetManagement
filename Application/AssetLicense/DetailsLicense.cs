using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AssetLicense
{
    public class DetailsLicense
    {
        public class Query : IRequest<List<AssetsLicense>>
        {
            public Guid LicenseId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<AssetsLicense>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<AssetsLicense>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var license = await _context.AssetsLicenses.Where(x => x.LicenseId == request.LicenseId).OrderByDescending(x => x.IssuedOn).ToListAsync();

                if (license == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found");

                return license;
            }
        }
    }
}