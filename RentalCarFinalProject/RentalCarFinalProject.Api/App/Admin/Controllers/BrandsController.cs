﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Core.Entities;
using RentalCarFinalProject.Service.DTOs.BrandDTOs;
using RentalCarFinalProject.Service.Extentions;
using RentalCarFinalProject.Service.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IWebHostEnvironment _env;
        public BrandsController(IBrandService brandService, IWebHostEnvironment env)
        {
            _brandService = brandService;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] BrandPostDTO brandPostDTO)
        {            
            await _brandService.PostAsync(brandPostDTO);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _brandService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _brandService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, [FromForm] BrandPutDTO brandPutDTO)
        {
            await _brandService.PutAsync(id,brandPutDTO);

            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            await _brandService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Restore(int? id)
        {
            await _brandService.RestoreAsync(id);
            return NoContent();
        }
    }
}
