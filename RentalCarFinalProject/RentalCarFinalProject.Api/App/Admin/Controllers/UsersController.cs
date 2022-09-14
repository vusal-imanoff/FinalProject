using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.DTOs.UserDTOs;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles =("SuperAdmin"))]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getalluser")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(string id)
        {
            return Ok(await _userService.GetById(id));
        }

        [HttpPost("createusers")]
        public async Task<IActionResult> Post(UserRegisterDTO userRegisterDTO)
        {
            await _userService.RegisterAsync(userRegisterDTO);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> IsActive(string id)
        {
            await _userService.ActiveAsync(id);
            return NoContent();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put(UserUpdateDTO userUpdateDTO)
        {
            await _userService.UpdateAsync(userUpdateDTO);
            return NoContent();
        }
    }
}
