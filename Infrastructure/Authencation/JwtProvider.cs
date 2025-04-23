using Application.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Infrastructure.Authencation
{
    internal class JwtProvider : IJwtProvider
    {
        private IConfiguration _configuration;
        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Genereate(ApplicationUser user)
        {
            var jwtOptions = _configuration.GetSection("jwt").Get<JwtOption>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.Key));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new System.Security.Claims.Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.UserName)
            };

            var token = new JwtSecurityToken(
                issuer: jwtOptions!.Issuer,
                audience: jwtOptions!.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(120),
                signingCredentials: credential);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class JwtOption
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
