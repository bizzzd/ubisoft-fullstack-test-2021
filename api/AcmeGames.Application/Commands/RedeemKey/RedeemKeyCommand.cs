using AcmeGames.Application.Common.Exceptions;
using AcmeGames.Application.Common.Interfaces;
using AcmeGames.Application.Queries.Ownership;
using AcmeGames.Domain;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeGames.Application.Commands.RedeemKey
{
    public class RedeemKeyCommand : IRequest<MyGameDto>
    {
        public string Key { get; set; }
    }

    public class RedeemKeyCommandHandler : IRequestHandler<RedeemKeyCommand, MyGameDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;

        public RedeemKeyCommandHandler(
            IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService, 
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<MyGameDto> Handle(RedeemKeyCommand request, CancellationToken cancellationToken)
        {
            var key = await _applicationDbContext.Keys.FirstOrDefaultAsync(k => k.Key == request.Key);
            if (key == null || key.IsRedeemed)
            {
                throw new ValidationException();
            }

            key.IsRedeemed = true;
            var ownership = new Ownership()
            {
                GameId = key.GameId,
                RegisteredDate = DateTime.UtcNow,
                State = OwnershipState.Owned,
                UserAccountId = _currentUserService.UserId
            };
            _applicationDbContext.Ownership.Add(ownership);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return await _applicationDbContext.Ownership
                .Where(o => o.OwnershipId == ownership.OwnershipId)
                .ProjectTo<MyGameDto>(_mapper.ConfigurationProvider)
                .FirstAsync();
        }
    }
}
