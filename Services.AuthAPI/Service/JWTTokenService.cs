using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.AuthAPI.Models;
using Services.AuthAPI.Service.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.AuthAPI.Service
{
    public class JWTTokenService : IJWTTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;
        public JWTTokenService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public string GenerateToken(ApplicationUser appuser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var algorithm=SecurityAlgorithms.HmacSha256Signature;
            var key = Encoding.ASCII.GetBytes(_jwtOptions.SecretKey);
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name,appuser.UserName.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,appuser.Id),
                new Claim(JwtRegisteredClaimNames.Email,appuser.Email),
            };

            var tokenDescription = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key), algorithm)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
