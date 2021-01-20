using IdentityApi.Data.Repositories;
using IdentityApi.Data.Repositories.Contracts;
using IdentityApi.Data.Services.Contracts;
using IdentityApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult> RefreshAsync(RefreshTokenRequestModel tokenApiModel)
        {
            if (tokenApiModel is null)
            {
                return BadRequest("Invalid client request");
            }

            var tokenApiResponseModel = await _tokenService.RefreshTokenAsync(tokenApiModel);

            if (tokenApiResponseModel is null)
            {
                return BadRequest("Invalid client request");
            }

            return Ok(tokenApiResponseModel);
        }

        [HttpPost]
        [Route("revoke")]
        [Authorize]
        public async Task<IActionResult> RevokeAsync()
        {
            bool isRevoked = await _tokenService.RevokeTokenAsync(User.Identity.Name);

            if (isRevoked)
            {
                return NoContent();
            }
            
            return BadRequest("User is not found");
        }
    }
}
