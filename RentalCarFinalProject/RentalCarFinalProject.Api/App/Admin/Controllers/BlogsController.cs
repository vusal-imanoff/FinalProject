using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.DTOs.BlogDTOs;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _blogService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _blogService.GetByIdAsync(id));
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromForm]BlogPostDTO blogPostDTO)
        {
            await _blogService.PostAsync(blogPostDTO);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id,[FromForm] BlogPutDTO blogPutDTO)
        {
            await _blogService.PutAsync(id, blogPutDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _blogService.DeleteAsync(id);
            return NoContent();
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _blogService.RestoreAsync(id);
            return NoContent();
        }
    }
}
