using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AssetLicense
{
    public class CreateAssetLicense
    {
        public class Command : IRequest
        {
            public Guid AssetsId { get; set; }
            public Guid LicenseId { get; set; }
            public DateTime IssuedOn { get; set; }
            public DateTime ReturnedOn { get; set; }
            public string IsActive { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.AssetsId).NotEmpty();
                RuleFor(x => x.LicenseId).NotEmpty();
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
                var assetsLicenses = await _context.AssetsLicenses.Where(x => x.LicenseId == request.LicenseId).AsNoTracking().ToListAsync();

                foreach (var item in assetsLicenses)
                {
                    item.IsActive = "No";
                }

                var assetsLicense = new AssetsLicense()
                {
                    Id = Guid.NewGuid(),
                    AssetId = request.AssetsId,
                    LicenseId = request.LicenseId,
                    IssuedOn = request.IssuedOn,
                    ReturnedOn = request.ReturnedOn,
                    IsActive = request.IsActive
                };

                _context.AssetsLicenses.Add(assetsLicense);

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