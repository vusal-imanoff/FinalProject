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

        public async Task<UserGetDTO> GetById(string id)
        {
            AppUser appuser = await _userManager.FindByIdAsync(id);
            if (appuser==null)
            {
                throw new NotFoundException("appuser not found");
            }

            UserGetDTO userGetDTO = _mapper.Map<UserGetDTO>(appuser);
            return userGetDTO;
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

        public async Task UpdateAsync(UserUpdateDTO userUpdateDTO)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userUpdateDTO.Id);
            if (appUser == null)
            {
                throw new NotFoundException("User not found");
            }

            appUser.Name=userUpdateDTO.Name;
            appUser.SurName=userUpdateDTO.Surname;
            appUser.Age=userUpdateDTO.Age;
            appUser.Email=userUpdateDTO.Email;
            appUser.UserName=userUpdateDTO.Username;


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
