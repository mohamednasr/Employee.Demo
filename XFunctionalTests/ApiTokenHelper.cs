using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace XFunctionalTests
{
    public class ApiTokenHelper
    {
        public static string getToken()
        {
            string userName = "admin";
            string[] roles = { "Admin" };

            return CreateToken(userName, roles);

        }

        private static string CreateToken(string userName, string[] roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ffc632ce-0053-4bab-8077-93a4d14caaad"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "admin@admin.com"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, "admin")

            };


            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var token = new JwtSecurityToken("test.com", "test.com", claims, null, expires: DateTime.Now.AddDays(2), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
