using System.Threading.Tasks;
using AcmeGames.Application.Commands.AuthenticateUser;
using AcmeGames.Application.Commands.ChangePassword;
using AcmeGames.Application.Commands.UpdatePersonalInfo;
using AcmeGames.Application.Queries.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcmeGames.API.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IUserQueries _userQueries;

        public AccountController(IUserQueries userQueries)
        {
            _userQueries = userQueries;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand authenticateUserCommand)
        {
            var result = await Mediator.Send(authenticateUserCommand);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            return Ok(result.Data);
        }

        [HttpGet]
        [Route("PersonalInfo")]
        public async Task<IActionResult> PersonalInfo()
        {
            return Ok(await _userQueries.GetCurrentUserPersonalInfo());
        }

        [HttpGet]
        [Route("ValidateEmail")]
        public async Task<IActionResult> ValidateEmail(string email)
        {
            return Ok(await _userQueries.IsEmailFreeToUse(email));
        }

        [HttpPut]
        [Route("PersonalInfo")]
        public async Task<IActionResult> UpdatePersonalInfo([FromBody] UpdatePersonalInfoCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }


        [HttpPut]
        [Route("Password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}