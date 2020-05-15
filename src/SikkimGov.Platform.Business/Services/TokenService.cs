using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SikkimGov.Platform.Business.Services.Contracts;
using SikkimGov.Platform.Models.ApiModels;

namespace SikkimGov.Platform.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly string Secret = "";
        public string GenerateJSONWebToken(AuthenticationResult loginResult)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);

            string email = loginResult.UserName;

            //IdentityModelEventSource.ShowPII = true;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                   {
                        new Claim(ClaimTypes.Name, Convert.ToString(loginResult.UserName)),
                        new Claim(ClaimTypes.GivenName, loginResult.UserName),
                        new Claim(ClaimTypes.Email, email),
                        //new Claim(ClaimTypes.Role, Convert.ToString(loginResult.Roles)),
                        new Claim(ClaimTypes.PrimarySid, Convert.ToString(loginResult.UserId)),
                        new Claim("IsSuperAdmin", loginResult.IsSuperAdmin.ToString()),
                        new Claim("IsAdmin", loginResult.IsAdmin.ToString()),
                        new Claim("UserID", Convert.ToString(loginResult.UserId)),
                        new Claim("IsDDO", Convert.ToString(loginResult.IsDDO)),
                        new Claim("IsRCO", Convert.ToString(loginResult.IsRCO)),
                        new Claim("DDOCode", Convert.ToString(loginResult.DDOCode)),
                        new Claim("DepartmentId", Convert.ToString(loginResult.DepartmentId)),
                   }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                NotBefore = DateTime.UtcNow
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
