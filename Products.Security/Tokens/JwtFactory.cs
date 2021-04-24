using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Products.Application.Interfaces;
using Products.Domain;
using Products.Helpers.ConfigModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Products.Security.Tokens
{
    public class JwtFactory : IJwtFactory
    {

        private readonly TokenSettings _tokenSettings;

        public JwtFactory(IOptions<TokenSettings> tokenSettings)
        {
            _tokenSettings = tokenSettings.Value;
        }

        public string Create(User user)
        {
            var claims = new List<Claim> { 
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(_tokenSettings.ExpirationInMinutes),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }
    }
}
