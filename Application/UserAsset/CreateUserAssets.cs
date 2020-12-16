using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
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
                var userAsset = new UserAssets()
                {
                    AssetsId = request.AssetsId,
                    UserStaffId = request.UserStaffId,
                    IssuedOn = request.IssuedOn,
                    ReturnedOn = request.ReturnedOn
                };

                _context.UserAssets.Add(userAsset);

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