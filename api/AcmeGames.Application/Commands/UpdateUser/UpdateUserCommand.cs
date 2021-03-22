using AcmeGames.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeGames.Application.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public string UserAccountId { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UpdateUserCommandHandler(
            IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToBeUpdated = await _applicationDbContext.Users
                .SingleAsync(u => u.UserAccountId == request.UserAccountId, cancellationToken);

            if (userToBeUpdated.FirstName != request.FirstName)
            {
                userToBeUpdated.FirstName = request.FirstName;
            }

            if (userToBeUpdated.LastName != request.LastName)
            {
                userToBeUpdated.LastName = request.LastName;
            }

            if (userToBeUpdated.EmailAddress != request.EmailAddress)
            {
                userToBeUpdated.EmailAddress = request.EmailAddress;
            }

            if (userToBeUpdated.DateOfBirth != request.DateOfBirth)
            {
                userToBeUpdated.DateOfBirth = request.DateOfBirth;
            }

            if (userToBeUpdated.IsAdmin != request.IsAdmin)
            {
                userToBeUpdated.IsAdmin = request.IsAdmin;
            }

            if (userToBeUpdated.Password != request.Password)
            {
                userToBeUpdated.Password = request.Password;
            }

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
