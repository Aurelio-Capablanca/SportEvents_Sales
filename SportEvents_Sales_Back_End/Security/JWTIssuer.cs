using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SportEvents_Sales_Back_End.Security
{
    public class JWTIssuer
    {

        public string GenerateToken(string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4b87b5d498a4b88462abd129d47179128809b05fc2a470ed29ffbd47b6af525a"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, "None")
            };
            var token = new JwtSecurityToken(
                issuer: "ehcl",
                audience: "front-end-sportsales",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(5),
                signingCredentials: credentials
                ); 
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
