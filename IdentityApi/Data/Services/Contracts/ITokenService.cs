using IdentityApi.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityApi.Data.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        Task<bool> RevokeTokenAsync(string userName);

        Task<TokenResponseModel> RefreshTokenAsync(RefreshTokenRequestModel tokenRequestModel);

        Task<TokenResponseModel> GenerateTokensAsync(UserRequestModel userRequestModel);
    }
}
