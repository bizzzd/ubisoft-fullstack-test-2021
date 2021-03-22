using AcmeGames.Application.Queries.User;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeGames.Application.Commands.UpdatePersonalInfo
{
    public class UpdatePersonalInfoCommandValidator : AbstractValidator<UpdatePersonalInfoCommand>
    {
        private readonly IUserQueries _userQueries;

        public UpdatePersonalInfoCommandValidator(IUserQueries userQueries)
        {
            _userQueries = userQueries;

            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("FirstName is required.");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("LastName is required.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MustAsync(BeUnique).WithMessage("The specified email already exists."); ;
        }

        private Task<bool> BeUnique(string email, CancellationToken cancellation)
        {
            return _userQueries.IsEmailFreeToUse(email);
        }
    }
}
