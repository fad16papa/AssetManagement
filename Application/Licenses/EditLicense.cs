using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.Licenses
{
    public class EditLicense
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string ProductName { get; set; }
            public string ProductVersion { get; set; }
            public string LicenseKey { get; set; }
            public string Expiration { get; set; }
            public DateTime ExpiredOn { get; set; }
            public string Remarks { get; set; }
            public string IsAvailable { get; set; }
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
                var license = await _context.Licenses.FindAsync(request.Id);

                if (license == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not found");
                }

                license.ProductName = request.ProductName ?? license.ProductName;
                license.ProductVersion = request.ProductVersion ?? license.ProductVersion;
                license.LicenseKey = request.LicenseKey ?? license.LicenseKey;
                license.Expiration = request.Expiration ?? license.Expiration;
                license.ExpiredOn = request.ExpiredOn;
                license.Remarks = request.Remarks ?? license.Remarks;
                license.IsAvailable = request.IsAvailable ?? license.IsAvailable;

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