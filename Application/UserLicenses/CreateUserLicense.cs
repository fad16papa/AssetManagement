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

namespace Application.UserLicenses
{
    public class CreateUserLicense
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

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.LicenseId).NotEmpty();
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
                var result = await _context.UserLicenses.Where(x => x.UserStaffId == request.UserStaffId && x.LicenseId == request.LicenseId
                && x.IsActive == request.IsActive).FirstOrDefaultAsync();

                if (result != null)
                    throw new RestException(HttpStatusCode.Conflict, "The user is already been assigned to this asset and its active");

                var userLicenses = await _context.UserLicenses.Where(x => x.LicenseId == request.LicenseId).ToListAsync();

                foreach (var item in userLicenses)
                {
                    item.IsActive = "No";
                }

                var userLicense = new UserLicense()
                {
                    Id = request.Id,
                    LicenseId = request.LicenseId,
                    UserStaffId = request.UserStaffId,
                    IssuedOn = request.IssuedOn,
                    ReturnedOn = request.ReturnedOn,
                    IsActive = request.IsActive
                };

                _context.UserLicenses.Add(userLicense);

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