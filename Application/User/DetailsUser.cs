using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.User
{
    public class DetailsUser
    {
        public class Query : IRequest<AppUser>
        {
            public string Email { get; set; }
        }

        public class Handler : IRequestHandler<Query, AppUser>
        {
            private readonly UserManager<AppUser> _userManager;
            public Handler(UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<AppUser> Handle(Query request, CancellationToken cancellationToken)
            {
                //handler logic goes here
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                    throw new RestException(HttpStatusCode.NotFound, "Not found");

                return user;
            }
        }
    }
}