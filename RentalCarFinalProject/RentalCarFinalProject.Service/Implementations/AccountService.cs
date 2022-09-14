using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.AppUserDTOs;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Interfaces;
using RentalCarFinalProject.Service.JWTManager.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtManager _jwtManager;

        public AccountService(IMapper mapper, UserManager<AppUser> userManager, IJwtManager jwtManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtManager = jwtManager;
        }



        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
             AppUser appUser= await _userManager.FindByEmailAsync(loginDTO.Email);
            if (await _userManager.IsInRoleAsync(appUser,"Member"))
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

            throw new UnauthorizedException("You do not access to enter");
        }

        public async Task RegisterAsync(RegisterDTO registerDTO)
        {   
            AppUser appUser = _mapper.Map<AppUser>(registerDTO);

           

                IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerDTO.Password);


            if (!identityResult.Succeeded   )
            {
                throw new BadRequestException(identityResult.Errors.ToString());
            }

            await _userManager.AddToRoleAsync(appUser, "Member");

        }

        public async Task ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            AppUser appUser = await _userManager.FindByIdAsync(resetPasswordDTO.Id);

            if (resetPasswordDTO.CurrentPassword != null)
            {
                if (resetPasswordDTO.NewPassword == null)
                {
                    throw new BadRequestException("Password is required");
                }

                if (!await _userManager.CheckPasswordAsync(appUser, resetPasswordDTO.CurrentPassword))
                {
                    throw new BadRequestException("current Password is incorrect");
                }

                IdentityResult identity = await _userManager.ChangePasswordAsync(appUser, resetPasswordDTO.CurrentPassword, resetPasswordDTO.NewPassword);

                if (!identity.Succeeded)
                {
                    foreach (var item in identity.Errors)
                    {
                        throw new BadRequestException(item.Description.ToString());
                    }
                }
            }
        }

        public async Task UpdateAsync(UpdateDTO updateDTO)
        {
            AppUser appUser = await _userManager.FindByIdAsync(updateDTO.Id);

            appUser.Name = updateDTO.Name;
            appUser.SurName = updateDTO.Surname;
            appUser.UserName = updateDTO.Username;
            appUser.Email = updateDTO.Email;
            appUser.Age = updateDTO.Age;

            IdentityResult identity = await _userManager.UpdateAsync(appUser);

            if (!identity.Succeeded)
            {
                foreach (var item in identity.Errors)
                {
                    throw new BadRequestException(item.Description.ToString());
                }
            }
        }
    }
}
