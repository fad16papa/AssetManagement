using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UserAsset
{
    public class ListUserAssets
    {
        public class Query : IRequest<List<UserAssets>>
        {

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
                var userAssets = await _context.UserAssets.ToListAsync();

                return userAssets;
            }
        }
    }
}