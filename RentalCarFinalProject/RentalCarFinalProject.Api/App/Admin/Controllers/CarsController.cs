using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.DTOs.CarDTOs;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
           return Ok(await _carService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _carService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CarPostDTO carPostDTO)
        {
            await _carService.PostAsync(carPostDTO);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id,[FromForm] CarPutDTO carPutDTO)
        {
            await _carService.PutAsync(id, carPutDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _carService.DeleteAsync(id);
            return NoContent();
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _carService.RestoreAsync(id);
            return NoContent();
        }
    }
}
