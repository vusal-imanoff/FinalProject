using Microsoft.AspNetCore.Identity;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.AppUserDTOs;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Interfaces;
using RentalCarFinalProject.Service.JWTManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtManager _jwtManager;

        public AdminService(UserManager<AppUser> userManager, IJwtManager jwtManager)
        {
            _userManager = userManager;
            _jwtManager = jwtManager;
        }
        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            AppUser appUser= await _userManager.FindByEmailAsync(loginDTO.Email);
            if (!await _userManager.IsInRoleAsync(appUser,"Member"))
            {
                if (appUser==null)
                {
                    throw new EmailOrPasswordInCorrectException("Email or passwod incorrect");
                }
                if (!await _userManager.CheckPasswordAsync(appUser, loginDTO.Password))
                {
                    throw new EmailOrPasswordInCorrectException("Email or passwod incorrect");
                }

                return await _jwtManager.GenerateTokenAsync(appUser);
            }

            throw new UnauthorizedException("You do not access to enter. You are Member!");
        }
    }
}
