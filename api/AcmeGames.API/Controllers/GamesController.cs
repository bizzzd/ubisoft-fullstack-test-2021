using AcmeGames.Application.Commands.RedeemKey;
using AcmeGames.Application.Queries.Ownership;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AcmeGames.API.Controllers
{
    public class GamesController : ApiControllerBase
    {
        private readonly IOwnershipQueries _ownershipQueries;

        public GamesController(IOwnershipQueries ownershipQueries)
        {
            _ownershipQueries = ownershipQueries;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _ownershipQueries.GetMyGames());
        }

        [HttpPost]
        [Route("RedeemKey")]
        public async Task<IActionResult> RedeemKey([FromBody] RedeemKeyCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
