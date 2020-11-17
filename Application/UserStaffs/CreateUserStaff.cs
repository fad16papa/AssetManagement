using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistence;
using Domain;

namespace Application.UserStaffs
{
    public class CreateUserStaff
    {
        public class Command : IRequest
        {
            public string DisplayName { get; set; }
            public string Department { get; set; }
            public string Location { get; set; }
            public DateTime DateCreated { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.Department).NotEmpty();
                RuleFor(x => x.Location).NotEmpty();
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
                var userStaff = new UserStaff()
                {
                    DisplayName = request.DisplayName,
                    Department = request.Department,
                    Location = request.Location,
                    DateCreated = DateTime.Now
                };

                _context.UserStaffs.Add(userStaff);

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