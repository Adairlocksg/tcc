using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Infra.Helpers
{
    public class TokenHelper(IHttpContextAccessor httpContextAccessor)
    {
        public static string Create(IConfiguration configuration, string role, Guid userId)
        {
            var key = Encoding.ASCII.GetBytes(configuration["Authentication:Key"]);

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new[]
                {
                        new Claim(ClaimTypes.Role, role),
                        new Claim("userId", userId.ToString())
                }),
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
