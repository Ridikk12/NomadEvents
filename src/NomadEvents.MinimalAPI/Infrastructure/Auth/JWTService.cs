﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NomadEvents.MinimalAPI.Infrastructure.Auth
{
    public class JwtService : IJwtService
    {
        private readonly string _signingKey;
        private readonly string _issuer;

        public JwtService(IConfiguration configuration)
        {
            _signingKey = configuration["Jwt:Secret"];
            _issuer = configuration["Jwt:Issuer"];
            
            ArgumentNullException.ThrowIfNull(_issuer);
            ArgumentNullException.ThrowIfNull(_signingKey);

        }

        public string GenerateJwt(List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_issuer,
                _issuer,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}