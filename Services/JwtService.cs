using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace githubSearch.Api.Services;

    //---------------
    // JwtService.cs - JWT Token Generation
    //---------------

public class JwtService
{
   private readonly string _key = "SUPER_SUPER_SECRET_KEY_123456789";


   // Generate JWT token
   public string GenerateToken(string username)
   {
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenKey = Encoding.ASCII.GetBytes(_key);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, username)
        }),
        Expires = DateTime.UtcNow.AddMinutes(30),
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(tokenKey),
            SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
   }
}
