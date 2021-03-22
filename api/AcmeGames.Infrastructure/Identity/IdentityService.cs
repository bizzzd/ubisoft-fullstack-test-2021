using AcmeGames.Application.Commands.AuthenticateUser;
using AcmeGames.Application.Common;
using AcmeGames.Application.Common.Interfaces;
using AcmeGames.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AcmeGames.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly SigningCredentials _signingCredentials;
        private readonly IApplicationDbContext _applicationDbContext;

        public IdentityService(
            IConfiguration configuration,
            IApplicationDbContext applicationDbContext)
        {
            var jwtKey = configuration[CommonConstants.JwtKeySettingName];

            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new ArgumentNullException($"{CommonConstants.JwtKeySettingName} settings is required");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Result<AuthenticationData>> Authenticate(string email, string password)
        {
            var user = await _applicationDbContext.Users.SingleOrDefaultAsync(u => u.EmailAddress == email && u.Password == password);

            if (user == null)
            {
                return Result<AuthenticationData>.Failure("Email or password is invalid");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserAccountId),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString("yyyy-MM-dd")),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            var token = new JwtSecurityToken(
                "localhost:56653",
                "localhost:56653",
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: _signingCredentials);

            return Result<AuthenticationData>.Success(
                new AuthenticationData
                {
                    Email = user.EmailAddress,
                    Name = user.FullName,     
                    IsAdmin = user.IsAdmin,
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
                });
        }
    }
}
