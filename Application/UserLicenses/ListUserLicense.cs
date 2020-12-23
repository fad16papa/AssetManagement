using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UserLicenses
{
    public class ListUserLicense
    {
        public class Query : IRequest<List<UserLicense>>
        {

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
                var userLicenses = await _context.UserLicenses.ToListAsync();

                return userLicenses;
            }
        }
    }
}