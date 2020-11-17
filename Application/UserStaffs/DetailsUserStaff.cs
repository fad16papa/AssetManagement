using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Persistence;

namespace Application.UserStaffs
{
    public class DetailsUserStaff
    {
        public class Query : IRequest<UserStaff>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, UserStaff>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<UserStaff> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here

                var userStaff = await _context.UserStaffs.FindAsync(request.Id);

                if (userStaff == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found");

                return userStaff;
            }
        }
    }
}