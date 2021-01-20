using IdentityApi.Data.Repositories;
using IdentityApi.Data.Repositories.Contracts;
using IdentityApi.Data.Services.Contracts;
using IdentityApi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IdentityApi.Data.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;

        public TokenService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(6),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || 
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public async Task<bool> RevokeTokenAsync(string userName)
        {
            var user = await _userRepository.FirstOrDefault(u => u.Username == userName);
            if (user is null)
            {
                return false;
            }
            user.RefreshToken = null;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<TokenResponseModel> RefreshTokenAsync(RefreshTokenRequestModel tokenRequestModel)
        {
            string accessToken = tokenRequestModel.AccessToken;
            string refreshToken = tokenRequestModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default

            var user = await _userRepository.FirstOrDefault(u => u.Username == username);

            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            var newAccessToken = GenerateAccessToken(principal.Claims);
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;

            await _userRepository.UpdateAsync(user);

            return new TokenResponseModel() 
            { 
                AccessToken = newAccessToken, 
                RefreshToken = newRefreshToken
            };
        }

        public async Task<TokenResponseModel> GenerateTokensAsync(UserRequestModel userRequestModel)
        {
            var user = await _userRepository.FirstOrDefault(u => string.Equals(u.Username, userRequestModel.Username) 
                                                        && string.Equals(u.Password, userRequestModel.Password));

            if (user != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "Manager")
                };
                var accessToken = GenerateAccessToken(claims);
                var refreshToken = GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(3);
                await _userRepository.UpdateAsync(user);

                return new TokenResponseModel()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            }
            else
            {
                return null;
            }
        }
    }
}
