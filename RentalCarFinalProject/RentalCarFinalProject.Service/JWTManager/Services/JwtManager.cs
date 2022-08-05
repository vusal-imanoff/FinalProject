using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.JWTManager.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.JWTManager.Services
{
    public class JwtManager : IJwtManager
    {
        private readonly UserManager<AppUser> _userManager;
        private IConfiguration Configuration { get; }

        public JwtManager( UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            Configuration = configuration;
        }

        public async Task<string> GenerateTokenAsync(AppUser appUser)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,appUser.Id),
                new Claim(ClaimTypes.Name,appUser.Name),
                new Claim(ClaimTypes.Email,appUser.Email)
            };

            IList<string> roles = await _userManager.GetRolesAsync(appUser);

            foreach (string role in roles)
            {
                Claim claim = new Claim(ClaimTypes.Role, role);
                claims.Add(claim);
            }

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JWT:SecurityKey").Value));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: Configuration.GetSection("JWT:Issuer").Value,
                audience: Configuration.GetSection("JWT:Audience").Value,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(4)
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            string token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }

    }
}
