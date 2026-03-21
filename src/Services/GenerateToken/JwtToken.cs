using System.Security.Claims;
using Campaign.API.Enums.Role;
using Campaign.API.Extensions.Enums;
using Campaign.API.Services.SecretKey;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Campaign.API.Services.GenerateToken
{
    public class JwtToken : IJwtToken
    {
        public string Generate(string name, Roles role)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new(new[]
               {
                  new Claim("name", name),
                  new Claim(ClaimTypes.Role, role.GetTranslatedDescription())
             }),

                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKeyService.GetBytes()), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
