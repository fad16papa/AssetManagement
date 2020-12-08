using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Persistence;

namespace Application.UserStaffs
{
    public class EditUserStaff
    {
        public class Command : IRequest
        {

            public Guid Id { get; set; }
            public string DisplayName { get; set; }
            public string Department { get; set; }
            public string Location { get; set; }
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
                var userStaff = await _context.UserStaffs.FindAsync(request.Id);

                if (userStaff == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found");

                userStaff.DisplayName = request.DisplayName ?? userStaff.DisplayName;
                userStaff.Department = request.Department ?? userStaff.Department;
                userStaff.Location = request.Location ?? userStaff.Location;
                userStaff.IsActive = request.IsActive ?? userStaff.IsActive;

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