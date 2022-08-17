using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.DTOs.OrderDTOs;
using RentalCarFinalProject.Service.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderService.GetAllAsync());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int? id, OrderPutDTO orderPutDTO)
        {
            await _orderService.PutAsync(id, orderPutDTO);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(await _orderService.GetByIdAsync(id));
        }

        [HttpPost("accept/{id}")]
        public async Task<IActionResult> Accept(int? id)
        {
            await _orderService.AcceptAsync(id);
            return NoContent();
        }

        [HttpPost("process/{id}")]
        public async Task<IActionResult> Process(int? id)
        {
            await _orderService.ProcessAsync(id);
            return NoContent();
        }

        [HttpPost("end/{id}")]
        public async Task<IActionResult> End(int? id)
        {
            await _orderService.EndAsync(id);
            return NoContent();
        }

        [HttpPost("reject/{id}")]
        public async Task<IActionResult> Reject(int? id)
        {
            await _orderService.RejectAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete(int? id)
        {
            await _orderService.DeleteAsync(id);
            return NoContent();
        }
    }
}
