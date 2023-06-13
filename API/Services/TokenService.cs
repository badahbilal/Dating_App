using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        {
            // Create a symmetric security key using the token key retrieved from the configuration settings.
            // The token key is expected to be a string representation of a sequence of bytes encoded in UTF-8.
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            // Create a list of claims to be included in the JWT payload.
            // In this case, a single claim is added, which represents the user's username.
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            // Create a new instance of the SigningCredentials class using the security key and algorithm.
            // The security key is used to sign the JWT, and the HmacSha512Signature algorithm is used.
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Create a new instance of the SecurityTokenDescriptor to configure the security token.
            // It specifies the subject (claims), expiration date, and signing credentials.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            // Create a new instance of JwtSecurityTokenHandler to handle JWT operations.
            // Use the token handler to create a token based on the provided token descriptor.
            // Finally, return the JWT as a string by writing the token using the token handler.
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}