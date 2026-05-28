using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WEBPEPE.Domain.Entities;
using WEBPEPE.Domain.Interfaces;

namespace WEBPEPE.Infrastructure.Security
{
    public class JwtService : IJwt
    {

        IConfiguration _configuration;
        public JwtService(IConfiguration configuritaion)
        {

            _configuration = configuritaion;

        }

        public object SegurityAlgorithms { get; private set; }

        public string GenerateToken(User user, IList<string> roles)
        {

            //Reclamaciones que identifican al usuario

            var claims = new List<Claim>() {

                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FullName),

            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));


            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(

                issuer: _configuration["JwtIssuer"],
                audience: _configuration["JwtAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtLifeTime"])),
                signingCredentials: creds


                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }





    }
}
