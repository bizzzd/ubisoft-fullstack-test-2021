using AcmeGames.Application.Common.Interfaces;
using AcmeGames.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeGames.Application.Commands.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<Result<AuthenticationData>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticationData
    {
        public string AccessToken { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Result<AuthenticationData>>
    {
        private readonly IIdentityService _identityService;

        public AuthenticateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public Task<Result<AuthenticationData>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            return _identityService.Authenticate(request.Username, request.Password);
        }
    }
}
