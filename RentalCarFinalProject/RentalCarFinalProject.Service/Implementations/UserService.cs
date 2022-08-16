using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalCarFinalProject.Core;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.UserDTOs;
using RentalCarFinalProject.Service.Exceptions;
using RentalCarFinalProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task ActiveAsync(string id)
        {
            if (id==null)
            {
                throw new BadRequestException("id is required");
            }
            AppUser appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null)
            {
                throw new NotFoundException("user not found");
            }

            if (appUser.IsActive)
            {
                appUser.IsActive = false;
            }
            else
            {
                appUser.IsActive = true;
            }

            await _unitOfWork.CommitAsync();
        }


        public async  Task<List<UserListDTO>> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();

            List<UserListDTO> userListDTOs = new List<UserListDTO>();
            foreach (var user in users)
            {
                userListDTOs.Add(_mapper.Map<UserListDTO>(user));
            }
            return userListDTOs;
        }

        public async Task RegisterAsync(UserRegisterDTO userRegisterDTO)
        {
            AppUser appUser = _mapper.Map<AppUser>(userRegisterDTO);

            IdentityResult identityResult = await _userManager.CreateAsync(appUser, userRegisterDTO.Password);

            if (userRegisterDTO.IsAdmin == true)
            {
                await _userManager.AddToRoleAsync(appUser, "Admin");
                appUser.IsAdmin=true;
            }
            else
            {
                await _userManager.AddToRoleAsync(appUser, "Member");

            }
            appUser.IsActive = true;

            if (!identityResult.Succeeded)
            {
                throw new BadRequestException(identityResult.Errors.ToString());
            }
        }
    }
}
