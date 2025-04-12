using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TCC.Infra.Helpers
{
    public class TokenHelper(IHttpContextAccessor httpContextAccessor): ITokenHelper
    {
        public static string Create(IConfiguration configuration, Guid userId)
        {
            var key = Encoding.ASCII.GetBytes(configuration["Authentication:Key"] ?? Environment.GetEnvironmentVariable("AuthenticationKey"));

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                        new Claim("userId", userId.ToString())
                ]),
                Expires = DateTime.MaxValue,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);

            return tokenHandler.WriteToken(token);
        }

        public Guid GetUserIdFromClaim()
        {
            var userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;
            return Guid.Parse(userId);
        }
    }
}
