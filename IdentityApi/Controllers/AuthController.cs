using IdentityApi.Data.Models;
using IdentityApi.Data.Repositories;
using IdentityApi.Data.Repositories.Contracts;
using IdentityApi.Data.Services.Contracts;
using IdentityApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IdentityApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] UserRequestModel userRequestModel)
        {
            if (userRequestModel == null)
            {
                return BadRequest("Invalid client request");
            }

            var tokenResponseModel = await _tokenService.GenerateTokensAsync(userRequestModel);

            if(tokenResponseModel != null)
            {
                return Ok(tokenResponseModel);
            }

            return Unauthorized();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserRequestModel user)
        {
            var users = await _userService.GetAsync();

            if (users.FirstOrDefault(u => u.Username == user.Username) != null)
            {
                return BadRequest("Invalid client request");
            }

            await _userService.CreateAsync(new User() 
            { 
                Username = user.Username, 
                Password = user.Password 
            });

            return Ok(new
            {
                User = user
            });
        }
    }
}
