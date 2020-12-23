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
    public class DetailsUser
    {
        public class Query : IRequest<List<UserLicense>>
        {
            public Guid UserId { get; set; }
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
                var users = await _context.UserLicenses.Where(x => x.UserStaffId == request.UserId).OrderByDescending(x => x.IssuedOn).ToListAsync();

                if (users == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found");

                return users;
            }
        }
    }
}