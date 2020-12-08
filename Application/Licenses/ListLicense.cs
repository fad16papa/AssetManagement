using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Licenses
{
    public class ListLicense
    {
        public class Query : IRequest<List<License>>
        {

        }

        public class Handler : IRequestHandler<Query, List<License>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<License>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var licenses = await _context.Licenses.ToListAsync();

                return licenses;
            }
        }
    }
}