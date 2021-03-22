using FluentValidation;

namespace AcmeGames.Application.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            RuleFor(c => c.Username)
                .NotEmpty().WithMessage("Email is required.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
