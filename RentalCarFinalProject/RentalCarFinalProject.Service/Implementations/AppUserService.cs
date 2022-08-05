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
    public class AppUserService : IAppUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtManager _jwtManager;

        public AppUserService(IMapper mapper, UserManager<AppUser> userManager, IJwtManager jwtManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _jwtManager = jwtManager;
        }



        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (appUser == null)
            {
                throw new BadRequestException("Email or passwod incorrect");
            }

            if (!await _userManager.CheckPasswordAsync(appUser,loginDTO.Password))
            {
                throw new BadRequestException("Email or passwod incorrect");
            }



            return await _jwtManager.GenerateTokenAsync(appUser) ;
        }

        public async Task RegisterAsync(RegisterDTO registerDTO)
        {   
            AppUser appUser = _mapper.Map<AppUser>(registerDTO);

            IdentityResult identityResult = await _userManager.CreateAsync(appUser,registerDTO.Password);

            if (!identityResult.Succeeded)
            {
                throw new BadRequestException(identityResult.Errors.ToString());
            }

            identityResult = await _userManager.AddToRoleAsync(appUser, "Admin");


        }
    }
}
