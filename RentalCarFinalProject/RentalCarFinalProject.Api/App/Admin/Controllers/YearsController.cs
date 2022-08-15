using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.DTOs.YearDTOs;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class YearsController : ControllerBase
    {
        private readonly IYearService _yearService;

        public YearsController(IYearService yearService)
        {
            _yearService = yearService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _yearService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _yearService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(YearPostDTO yearPostDTO)
        {
            await _yearService.PostAsync(yearPostDTO);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, YearPutDTO yearPutDTO)
        {
            await _yearService.PutAsync(id, yearPutDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _yearService.DeleteAsync(id);
            return NoContent();
        }

    }
}
