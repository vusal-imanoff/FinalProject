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
    }
}
