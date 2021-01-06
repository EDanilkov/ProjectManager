using IdentityApi.Data.Repositories;
using IdentityApi.Data.Repositories.Contracts;
using IdentityApi.Data.Services.Contracts;
using IdentityApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IdentityApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IUserRepository userRepository;
        readonly ITokenService tokenService;

        public LoginController(ProjectManagerContext db, ITokenService tokenService)
        {
            userRepository = new UserRepository(db);
            this.tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] User loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid client request");
            }

            var users = userRepository.Get();
            var user = users.FirstOrDefault(u => (u.Username == loginModel.Username) &&
                                        (u.Password == loginModel.Password));

            if (user != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "Manager")
                };
                var accessToken = tokenService.GenerateAccessToken(claims);
                var refreshToken = tokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(3);
                await userRepository.UpdateAsync(user);

                return Ok(new
                {
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    UserId = user.Id
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            var users = userRepository.Get();
            if(users.FirstOrDefault(u => (u.Username == user.Username)) != null)
            {
                return BadRequest("Invalid client request 1");
            }

            await userRepository.CreateAsync(user);

            return Ok(new
            {
                User = user
            });
        }

        private static string GenerateHashPass(string password)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);

            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            string encoded = BitConverter.ToString(hash)
               .Replace("-", string.Empty)
               .ToLower();

            return encoded;
        }
    }
}
