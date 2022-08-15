using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.DTOs.CategoryDTOs;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoryPostDTO categoryPostDTO)
        {
            await _categoryService.PostAsync(categoryPostDTO);
            return StatusCode(201);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {            
            return Ok(await _categoryService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            return Ok(await _categoryService.GetByIdAsync(id));
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, CategoryPutDTO categoryPutDTO)
        {
            await _categoryService.PutAsync(id, categoryPutDTO);
            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _categoryService.DeleteAsync(id);
            return StatusCode(204);
        }

    }
}
