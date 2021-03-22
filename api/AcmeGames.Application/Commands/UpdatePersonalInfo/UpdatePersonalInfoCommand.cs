using AcmeGames.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeGames.Application.Commands.UpdatePersonalInfo
{
    public class UpdatePersonalInfoCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UpdatePersonalInfoCommandHandler : IRequestHandler<UpdatePersonalInfoCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;

        public UpdatePersonalInfoCommandHandler(
            IApplicationDbContext applicationDbContext, 
            ICurrentUserService currentUserService)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(UpdatePersonalInfoCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId;
            var userToBeUpdated = await _applicationDbContext.Users
                .SingleAsync(u => u.UserAccountId == currentUserId, cancellationToken);

            if (userToBeUpdated.FirstName != request.FirstName)
            {
                userToBeUpdated.FirstName = request.FirstName;
            }

            if (userToBeUpdated.LastName != request.LastName)
            {
                userToBeUpdated.LastName = request.LastName;
            }

            if (userToBeUpdated.EmailAddress != request.Email)
            {
                userToBeUpdated.EmailAddress = request.Email;
            }

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
