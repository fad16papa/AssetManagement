using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UserLicenses
{
    public class EditUserLicense
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid LicenseId { get; set; }
            public Guid UserStaffId { get; set; }
            public DateTime IssuedOn { get; set; }
            public DateTime ReturnedOn { get; set; }
            public string IsActive { get; set; }
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
                var userAssets = await _context.UserLicenses.Where(x => x.LicenseId == request.LicenseId).OrderByDescending(x => x.IssuedOn).ToListAsync();

                if (userAssets == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not found");
                }
                if (userAssets.Count() == 0)
                {
                    return Unit.Value;
                }

                foreach (var item in userAssets)
                {
                    item.IsActive = "No";
                }

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