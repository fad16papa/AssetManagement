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
    public class EditAsset
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string HostName { get; set; }
            public string SerialNo { get; set; }
            public string ExpressCode { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Type { get; set; }
            public string Status { get; set; }
            public string Location { get; set; }        
            public string IsAvailable { get; set; }
            public string Remarks { get; set; }
            public DateTime? IssuedOn { get; set; }
            public DateTime? ReturnedOn { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                //logic goes here
                var asset = await _context.Assets.FindAsync(request.Id);

                if (asset == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not found");
                }

                asset.HostName = request.HostName ?? asset.HostName;
                asset.SerialNo = request.SerialNo ?? asset.SerialNo;
                asset.ExpressCode = request.ExpressCode ?? asset.ExpressCode;
                asset.Brand = request.Brand ?? asset.Brand;
                asset.Model = request.Model ?? asset.Model;
                asset.Type = request.Type ?? asset.Type;
                asset.Status = request.Status ?? asset.Status;
                asset.Location = request.Location ?? asset.Location;
                asset.IssuedOn = request.IssuedOn ?? asset.IssuedOn;
                asset.ReturnedOn = request.ReturnedOn ?? asset.ReturnedOn;

                var success = await _context.SaveChangesAsync() > 0;

                if (success)
                {
                    return Unit.Value;
                }
                else
                {
                    throw new Exception("Problem saving changes");
                }
            }
        }
    }
}