using Microsoft.AspNetCore.Http;
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
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountsController(RoleManager<IdentityRole> roleManager, IAccountService accountService, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _roleManager = roleManager;
            _accountService = accountService;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            await _accountService.RegisterAsync(registerDTO);
            AppUser appUser = await _userManager.FindByEmailAsync(registerDTO.Email);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var link = Url.Action("ConfirmEmail", "Accounts", new { userId = appUser.Id, token = code }, Request.Scheme, Request.Host.ToString());
            _emailService.Register(registerDTO, link);
            return Ok();
        }

        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, token);
            return Ok();

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
