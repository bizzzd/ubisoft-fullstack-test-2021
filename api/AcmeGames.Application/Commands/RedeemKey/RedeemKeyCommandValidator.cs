using AcmeGames.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeGames.Application.Commands.RedeemKey
{
    public class RedeemKeyCommandValidator : AbstractValidator<RedeemKeyCommand>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public RedeemKeyCommandValidator(IApplicationDbContext applicationDbContext)
        {
           _applicationDbContext = applicationDbContext;

            RuleFor(c => c.Key)
                .NotEmpty().WithMessage("Key is required.")
                .MustAsync(BeValid).WithMessage("The specified key is invalid or has been already redeemed."); ;
        }

        private Task<bool> BeValid(string key, CancellationToken cancellation)
        {
            return _applicationDbContext.Keys.AnyAsync(k => k.Key == key && !k.IsRedeemed);
        }
    }
}
