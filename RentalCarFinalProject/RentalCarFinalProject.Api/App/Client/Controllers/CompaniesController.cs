using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _company;

        public CompaniesController(ICompanyService company)
        {
            _company = company;
        }

        [HttpGet("companies/getall/{int}")]
        public async Task<IActionResult> GetPaginatedAll(int pageIndex = 1)
        {
            return Ok( await _company.GetAllPageIndexAsync(pageIndex));
        }

        [HttpGet("companies/{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _company.GetByIdAsync(id));
        }
    }
}
