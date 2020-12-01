using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Assets
{
    public class CreateAsset
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string HostName { get; set; }
            public string SerialNo { get; set; }
            public string ExpressCode { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Type { get; set; }
            public string Status { get; set; }
            public string Location { get; set; }
            public string IsAvailable { get; set; }
            public string Remarks { get; set; }
            public DateTime IssuedOn { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.HostName).NotEmpty();
                RuleFor(x => x.SerialNo).NotEmpty();
                RuleFor(x => x.ExpressCode).NotEmpty();
                RuleFor(x => x.Brand).NotEmpty();
                RuleFor(x => x.Model).NotEmpty();
                RuleFor(x => x.Type).NotEmpty();
                RuleFor(x => x.Status).NotEmpty();
                RuleFor(x => x.Location).NotEmpty();
                RuleFor(x => x.IsAvailable).NotEmpty();
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
                var asset = new Asset()
                {
                    Id = request.Id,
                    HostName = request.HostName,
                    SerialNo = request.SerialNo,
                    ExpressCode = request.ExpressCode,
                    Brand = request.Brand,
                    Model = request.Model,
                    Type = request.Type,
                    Status = request.Status,
                    IsAvailable = request.IsAvailable,
                    Remarks = request.Remarks,
                    Location = request.Location,
                    IssuedOn = request.IssuedOn
                };

                _context.Assets.Add(asset);

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