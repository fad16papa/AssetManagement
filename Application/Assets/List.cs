using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Assets
{
    public class List
    {
        public class Query : IRequest<List<Asset>>
        {

        }

        public class Handler : IRequestHandler<Query, List<Asset>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Asset>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var assets = await _context.Assets.ToListAsync();

                return assets;
            }
        }
    }
}