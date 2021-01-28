using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UserAsset
{
    public class EditUserAssets : IRequest
    {
        public class Command : IRequest
        {
            public Guid AssetsId { get; set; }
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
                var userAssets = await _context.UserAssets.Where(x => x.AssetsId == request.AssetsId).OrderByDescending(x => x.IssuedOn).ToListAsync();

                if (userAssets == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, "Not found");
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