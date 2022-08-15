using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.DTOs.EngineDTOs;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class EnginesController : ControllerBase
    {
        private readonly IEngineService _engineService;

        public EnginesController(IEngineService engineService)
        {
            _engineService = engineService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _engineService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _engineService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(EnginePostDTO enginePostDTO)
        {
            await _engineService.PostAsync(enginePostDTO);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, EnginePutDTO enginePutDTO)
        {
            await _engineService.PutAsync(id, enginePutDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _engineService.DeleteAsync(id);
            return NoContent();
        }
    }
}
