using AcmeGames.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeGames.Application.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordRepeat { get; set; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(
            IApplicationDbContext applicationDbContext, 
            ICurrentUserService currentUserService)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.UserId;
            var userToBeUpdated = await _applicationDbContext.Users
                .SingleAsync(u => u.UserAccountId == currentUserId, cancellationToken);

            userToBeUpdated.Password = request.NewPassword;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
