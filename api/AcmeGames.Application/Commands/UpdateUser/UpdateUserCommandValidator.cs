using AcmeGames.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeGames.Application.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public UpdateUserCommandValidator(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

            RuleFor(c => c.UserAccountId)
                .NotEmpty().WithMessage("UserAccountId is required.")
                .MustAsync(Exist).WithMessage("User should exist");

            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("FirstName is required.");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("LastName is required.");

            RuleFor(c => c.EmailAddress)
                .NotEmpty().WithMessage("Email is required.")
                .MustAsync(BeUnique).WithMessage("The specified email already exists."); ;
        }

        private async Task<bool> Exist(string userId, CancellationToken cancellation)
        {
            return await _applicationDbContext.Users.AnyAsync(u => u.UserAccountId == userId);
        }

        private async Task<bool> BeUnique(UpdateUserCommand command, string email, CancellationToken cancellation)
        {
            return !(await _applicationDbContext.Users
                .AnyAsync(u => u.EmailAddress == email && u.UserAccountId != command.UserAccountId));
        }
    }
}
