using System.Security.Claims;
using Campaign.API.Enums.Role;
using Campaign.API.Extensions.Role;
using Campaign.API.Service.SecretKey;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Campaign.API.Service.GenerateToken
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
                  new Claim(ClaimTypes.Role, role.RoleToDesc().ToLower())
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
