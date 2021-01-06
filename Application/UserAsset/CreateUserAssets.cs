using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UserAsset
{
    public class CreateUserAssets
    {
        public class Command : IRequest
        {
            public Guid AssetsId { get; set; }
            public Guid UserStaffId { get; set; }
            public DateTime IssuedOn { get; set; }
            public DateTime ReturnedOn { get; set; }
            public string IsActive { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AssetsId).NotEmpty();
                RuleFor(x => x.UserStaffId).NotEmpty();
            }
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
                //Check the userId and AssetsId is already been created and if its IsActive to Yes
                // var result = await _context.UserAssets.Where(x => x.UserStaffId == request.UserStaffId && x.AssetsId == request.AssetsId
                // && x.IsActive == request.IsActive).FirstOrDefaultAsync();

                // if (result != null)
                //     throw new RestException(HttpStatusCode.Conflict, "The user is already been assigned to this asset and its active");

                var userAssets = await _context.UserAssets.Where(x => x.AssetsId == request.AssetsId).AsNoTracking().ToListAsync();

                foreach (var item in userAssets)
                {
                    item.IsActive = "No";
                }

                foreach (var item in userAssets.Where(x => !_context.UserAssets.Any(ua => x.AssetsId == ua.AssetsId && x.UserStaffId == ua.UserStaffId)))
                {
                    var userAsset = new UserAssets()
                    {
                        Id = Guid.NewGuid(),
                        AssetsId = request.AssetsId,
                        UserStaffId = request.UserStaffId,
                        IssuedOn = request.IssuedOn,
                        ReturnedOn = request.ReturnedOn,
                        IsActive = request.IsActive
                    };

                    _context.UserAssets.Add(item);

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