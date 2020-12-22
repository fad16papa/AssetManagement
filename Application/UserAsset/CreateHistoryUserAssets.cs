using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.UserAsset
{
    public class CreateHistoryUserAssets
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                var hsitoryUserAssets = new HistoryUserAssets()
                {
                    Id = request.Id,
                    AssetsId = request.AssetsId,
                    UserStaffId = request.UserStaffId,
                    IssuedOn = request.IssuedOn,
                    ReturnedOn = request.ReturnedOn,
                    IsActive = request.IsActive
                };

                _context.HistoryUserAssets.Add(hsitoryUserAssets);

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