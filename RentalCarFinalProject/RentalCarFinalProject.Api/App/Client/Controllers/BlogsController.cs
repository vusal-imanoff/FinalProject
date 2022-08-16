using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("getblogs")]
        public async Task<IActionResult> Blogs()
        {
            return Ok(await _blogService.GetAllAsync());
        }
        [HttpGet("blogs/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            return Ok(await _blogService.GetByIdAsync(id));
        }
    }
}
