using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.DTOs.UserDTOs;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =("SuperAdmin"))]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpPost("createusers")]
        public async Task<IActionResult> Post(UserRegisterDTO userRegisterDTO)
        {
            await _userService.RegisterAsync(userRegisterDTO);
            return StatusCode(201);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> IsActive(string id)
        {
            await _userService.ActiveAsync(id);
            return NoContent();
        }
    }
}
