using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet("getcars/{int}")]
        public async Task<IActionResult> GetCarsPageneted(int pageIndex=1)
        {
            return Ok(await _carService.GetAllForUsersAsync(pageIndex));
        }

        [HttpGet("getcars")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _carService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            return Ok(await _carService.GetByIdAsync(id));
        }
    }
}
