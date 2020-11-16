using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.Assets
{
    public class DetailsAsset
    {
        public class Query : IRequest<Asset>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Asset>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Asset> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var asset = await _context.Assets.FindAsync(request.Id);

                if (asset == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not found");
                }

                return asset;
            }
        }
    }
}