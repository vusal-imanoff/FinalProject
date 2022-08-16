using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.DTOs.AppUserDTOs;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountsController(RoleManager<IdentityRole> roleManager, IAccountService accountService)
        {
            _roleManager = roleManager;
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            await _accountService.RegisterAsync(registerDTO);
            return StatusCode(201);
        }


        [HttpPost("login")]
        public async  Task<IActionResult> Login(LoginDTO loginDTO)
        {

            return Ok( await _accountService.LoginAsync(loginDTO));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Put(UpdateDTO updateDTO)
        {
            await _accountService.UpdateAsync(updateDTO);
            return NoContent();
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            await _accountService.ResetPasswordAsync(resetPasswordDTO);
            return NoContent();
        }

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
