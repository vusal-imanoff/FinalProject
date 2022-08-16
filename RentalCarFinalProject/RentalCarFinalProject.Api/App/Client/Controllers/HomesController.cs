using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomesController : ControllerBase
    {
        private readonly ISliderService _sliderService;
        private readonly IBlogService _blogService;

        public HomesController(ISliderService sliderService, IBlogService blogService)
        {
            _sliderService = sliderService;
            _blogService = blogService;
        }

        [HttpGet("getsliders")]
        public async Task<IActionResult> Sliders()
        {
            return Ok(await _sliderService.GetAllAsync());
        }

        [HttpGet("getblogs")]
        public async Task<IActionResult> Blogs()
        {
            return Ok(await _blogService.GetAllAsync());
        }
    }
}
