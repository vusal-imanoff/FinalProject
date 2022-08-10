using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.DTOs.TagDTOs;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagsService;

        public TagsController(ITagService tagsService)
        {
            _tagsService = tagsService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tagsService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _tagsService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(TagPostDTO tagPostDTO)
        {
            await _tagsService.PostAsync(tagPostDTO);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id,TagPutDTO tagPutDTO)
        {
            await _tagsService.PutAsync(id, tagPutDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _tagsService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _tagsService.RestoreAsync(id);
            return NoContent();
        }
    }
}
