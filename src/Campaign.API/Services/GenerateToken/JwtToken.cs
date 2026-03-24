using System.Security.Claims;
using Campaign.API.Enums.Role;
using Campaign.API.Extensions.Enums;
using Microsoft.IdentityModel.Tokens;
using Campaign.API.Services.SecretKey;
using System.IdentityModel.Tokens.Jwt;

namespace Campaign.API.Services.GenerateToken
{
    public class JwtToken : IJwtToken
    {
        public string Generate(string userId, string name, string? teamId, Roles role)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new(new[]
               {
                  new Claim("id", userId),
                  new Claim("name", name),
                  new Claim(ClaimTypes.Role, role.GetTranslatedDescription()),
             }),

                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKeyService.GetBytes()), SecurityAlgorithms.HmacSha256Signature)
            };

            if (!string.IsNullOrEmpty(teamId))
                tokenDescriptor.Subject.AddClaim(new Claim("teamId", teamId));

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
