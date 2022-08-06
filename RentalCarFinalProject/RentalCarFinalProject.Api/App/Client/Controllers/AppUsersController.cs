﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.AppUserDTOs;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppUsersController(RoleManager<IdentityRole> roleManager, IAppUserService appUserService)
        {
            _roleManager = roleManager;
            _appUserService = appUserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            await _appUserService.RegisterAsync(registerDTO);
            return StatusCode(201);
        }

        /// <summary>
        /// Sisteme Daxil Olmaq Service
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/accounts/login
        ///     {        
        ///       "email": "superadmin@rental.az",
        ///       "password": "Super@123"      
        ///     }
        /// </remarks>
        /// <param name="loginDto"></param>
        /// <returns>response olaraq token qaytarir</returns>
        /// <response code="200">Successfuly Login</response>
        /// <response code="400">Email Or Password Is InCorred</response>         
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
           
            return Ok(await _appUserService.LoginAsync(loginDTO));
        }

        //[HttpGet("{username}")]
        //public async Task<IActionResult> Get(string? username)
        //{
        //    return Ok(await _appUserService.GetAsync(username));
        //}

        #region CreateRole

        //[HttpGet("createrole")]
        //public async Task<IActionResult> CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
        //    return Ok();
        //}
        #endregion

    }


}
