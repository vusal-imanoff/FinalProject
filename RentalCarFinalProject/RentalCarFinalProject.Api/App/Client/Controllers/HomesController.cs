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
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;

        public HomesController(ISliderService sliderService, IBlogService blogService, ICategoryService categoryService, IBrandService brandService)
        {
            _sliderService = sliderService;
            _blogService = blogService;
            _categoryService = categoryService;
            _brandService = brandService;
        }
        [HttpGet("getbrands")]
        public async Task<IActionResult> SearchBrand()
        {
            return Ok(await _brandService.GetAllForUsersAsync());
        }

        [HttpGet("getcategories")]
        public async Task<IActionResult> SearchCategory()
        {
            return Ok(await _categoryService.GetAllForUsersAsync());
        }

        [HttpGet("getsliders")]
        public async Task<IActionResult> Sliders()
        {
            return Ok(await _sliderService.GetAllForUsersAsync());
        }

        [HttpGet("getblogs")]
        public async Task<IActionResult> Blogs()
        {
            return Ok(await _blogService.GetAllForUsersAsync());
        }
    }
}
