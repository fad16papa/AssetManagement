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

namespace Application.UserAsset
{
    public class DetailsUser
    {
        public class Query : IRequest<List<UserAssets>>
        {
            public Guid UserId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<UserAssets>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<UserAssets>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var users = await _context.UserAssets.Where(x => x.UserStaffId == request.UserId).OrderByDescending(x => x.IssuedOn).ToListAsync();

                if (users == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found");

                return users;
            }
        }
    }
}