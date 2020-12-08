using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Licenses
{
    public class DetailsLicense
    {
        public class Query : IRequest<License>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, License>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<License> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var license = await _context.Licenses.FindAsync(request.Id);

                if (license == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found");

                return license;
            }
        }
    }
}