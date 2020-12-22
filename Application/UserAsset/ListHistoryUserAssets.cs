using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UserAsset
{
    public class ListHistoryUserAssets
    {
        public class Query : IRequest<List<HistoryUserAssets>>
        {

        }

        public class Handler : IRequestHandler<Query, List<HistoryUserAssets>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<HistoryUserAssets>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var historyUserAssets = await _context.HistoryUserAssets.ToListAsync();

                return historyUserAssets;
            }
        }
    }
}