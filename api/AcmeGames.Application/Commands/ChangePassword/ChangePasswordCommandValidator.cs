using AcmeGames.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeGames.Application.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _applicationDbContext;

        public ChangePasswordCommandValidator(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;

            RuleFor(c => c.CurrentPassword)
                .NotEmpty().WithMessage("CurrentPassword is required.")
                .MustAsync(MatchWithActualPassword).WithMessage("Current password is invalid."); ;

            RuleFor(c => c.NewPassword)
                .NotEmpty().WithMessage("NewPassword is required.");

            RuleFor(c => c.NewPasswordRepeat)
                .NotEmpty().WithMessage("NewPasswordRepeat is required.")
                .Matches(c => c.NewPassword).WithMessage("Passwords should match.");
        }

        private Task<bool> MatchWithActualPassword(string providedPassword, CancellationToken cancellation)
        {
            return _applicationDbContext.Users.AnyAsync(
                u => u.UserAccountId == _currentUserService.UserId && u.Password == providedPassword, cancellation);
        }
    }
}
