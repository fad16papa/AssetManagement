using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UserStaffs
{
    public class ListUserStaffs
    {
        public class Query : IRequest<List<UserStaff>>
        {

        }

        public class Handler : IRequestHandler<Query, List<UserStaff>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<UserStaff>> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var userStaffs = await _context.UserStaffs.ToListAsync();

                return userStaffs;
            }
        }
    }
}