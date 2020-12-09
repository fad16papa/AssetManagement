using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Licenses
{
    public class CreateLicense
    {
        public class Command : IRequest
        {
            public string ProductName { get; set; }
            public string ProductVersion { get; set; }
            public string LicenseKey { get; set; }
            public string Expiration { get; set; }
            public DateTime ExpiredOn { get; set; }
            public string Remarks { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ProductName).NotEmpty();
                RuleFor(x => x.ProductVersion).NotEmpty();
                RuleFor(x => x.LicenseKey).NotEmpty();
                RuleFor(x => x.Expiration).NotEmpty();
                RuleFor(x => x.ExpiredOn).NotEmpty();
                RuleFor(x => x.Remarks).NotEmpty();
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
                var license = new License()
                {
                    ProductName = request.ProductName,
                    ProductVersion = request.ProductVersion,
                    LicenseKey = request.LicenseKey,
                    Expiration = request.Expiration,
                    ExpiredOn = request.ExpiredOn,
                    Remarks = request.Remarks
                };

                _context.Licenses.Add(license);

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