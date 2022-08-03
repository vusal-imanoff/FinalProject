using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.DTOs.FuelDTOs;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class FuelsController : ControllerBase
    {
        private readonly IFuelService _fuelService;

        public FuelsController(IFuelService fuelService)
        {
            _fuelService = fuelService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _fuelService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _fuelService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(FuelPostDTO fuelPostDTO)
        {
            await _fuelService.PostAsync(fuelPostDTO);
            return StatusCode(201);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Put(int? id, FuelPutDTO fuelPutDTO)
        {
            await _fuelService.PutAsync(id, fuelPutDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _fuelService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _fuelService.RestoreAsync(id);
            return NoContent();
        }
    }
}
