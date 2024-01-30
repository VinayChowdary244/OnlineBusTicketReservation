using BusTicketingWebApplication.Interfaces;
using BusTicketingWebApplication.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusTicketingWebApplication.Services
{
    // TokenService class responsible for generating JWT tokens
    public class TokenService : ITokenService
    {
        // Symmetric key used for token signing
        private readonly SymmetricSecurityKey _key;

        // Constructor to initialize the SymmetricSecurityKey with a secret key from the configuration
        public TokenService(IConfiguration configuration)
        {
            // Get secret key from configuration
            var secretKey = configuration["SecretKey"].ToString();

            // Convert the secret key to bytes and create a SymmetricSecurityKey
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        }

        // Method to generate a JWT token based on user information
        public string GetToken(UserDTO user)
        {
            // Claims represent information about the user to be included in the token
            var claims = new List<Claim>()
            {
                // Include user's name in the token
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                
                // Include user's role in the token
                new Claim("role", user.Role)
            };

            // Signing credentials used to sign the token with the SymmetricSecurityKey
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Token description specifying token details
            var tokenDescription = new SecurityTokenDescriptor
            {
                // Set the subject of the token to contain the claims
                Subject = new ClaimsIdentity(claims),

                // Set the expiration time of the token (1 day in this case)
                Expires = DateTime.Now.AddDays(1),

                // Set the signing credentials for token security
                SigningCredentials = cred
            };

            // Token handler responsible for creating and writing the token
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
