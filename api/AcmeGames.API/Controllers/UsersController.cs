using AcmeGames.Application.Commands.UpdateUser;
using AcmeGames.Application.Queries.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AcmeGames.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserQueries _userQueries;

        public UsersController(IUserQueries userQueries)
        {
            _userQueries = userQueries;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _userQueries.GetUsers());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.UserAccountId)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
