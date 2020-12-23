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

namespace Application.UserLicenses
{
    public class DetailsLicense
    {
        public class Query : IRequest<List<UserLicense>>
        {
            public Guid LicenseId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<UserLicense>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<UserLicense>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var license = await _context.UserLicenses.Where(x => x.LicenseId == request.LicenseId).OrderByDescending(x => x.IssuedOn).ToListAsync();

                if (license == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found");

                return license;
            }
        }
    }
}