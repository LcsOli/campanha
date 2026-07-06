using System.Security.Claims;
using Campaign.API.Services.SecretKey;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Campaign.API.Services.Token
{
    public class JwtToken : IJwtToken
    {
        public string Generate(string userId, string name, string teamId, string sellerId, string role)
        {
            return Token(new SecurityTokenDescriptor
            {
                Subject = new([

                  new Claim("id", userId),
                  new Claim("name", name),
                  new Claim("teamId", teamId),
                  new Claim("sellerId", sellerId),
                  new Claim(ClaimTypes.Role, role.ToString().ToLower())
               ])
            });
        }

        public string Generate(string userId, string name, string supplierId, string role)
        {
            return Token(new SecurityTokenDescriptor
            {
                Subject = new([

                  new Claim("id", userId),
                  new Claim("name", name),
                  new Claim("supplierId", supplierId),
                  new Claim(ClaimTypes.Role, role)
               ])
            });
        }

        public string Generate(string userId, string name, int sellerId, string role)
        {
            return Token(new SecurityTokenDescriptor
            {
                Subject = new([

                  new Claim("id", userId),
                  new Claim("name", name),
                  new Claim("sellerId", sellerId.ToString()),
                  new Claim(ClaimTypes.Role, role.ToString().ToLower()),
               ])
            });
        }

        private static string Token(SecurityTokenDescriptor tokenDescriptor)
        {
            tokenDescriptor.Expires = DateTime.UtcNow.AddHours(8);
            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKeyService.GetBytes()), SecurityAlgorithms.HmacSha256Signature);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
