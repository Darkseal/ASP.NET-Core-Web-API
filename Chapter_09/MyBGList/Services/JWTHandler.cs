using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyBGList.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyBGList.Services
{
    public class JWTHandler
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApiUser> _userManager;

        public JWTHandler(
            IConfiguration configuration,
            UserManager<ApiUser> userManager
            )
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<JwtSecurityToken> GetTokenAsync(ApiUser user)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"])),
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.AddRange((await _userManager.GetRolesAsync(user))
                .Select(r => new Claim(ClaimTypes.Role, r)));

            return new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(
                    _configuration["JWT:ExpirationTimeInMinutes"])),
                signingCredentials: signingCredentials);
        }
    }
}
