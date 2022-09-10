using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarFinalProject.Service.DTOs.OrderDTOs;
using RentalCarFinalProject.Service.Interfaces;
using RentalCarFinalProject.Service.JWTManager.Interfaces;
using System.Threading.Tasks;

namespace RentalCarFinalProject.Api.App.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IJwtManager _jwtManager;

        public OrdersController(IOrderService orderService, IJwtManager jwtManager)
        {
            _orderService = orderService;
            _jwtManager = jwtManager;
        }

        [HttpPost("order")]
        public  async Task<IActionResult> Post(OrderPostDTO orderPostDTO)
        {
            await _orderService.PostAsync(orderPostDTO);
            return StatusCode(201);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            string user = _jwtManager.GetUserNameByToken(Request.Headers["Authorization"].ToString().Split(" ")[1]);
            return Ok(await _orderService.GetAllByUsernameAsync(user));
        }

        [HttpGet("getorders/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            string user = _jwtManager.GetUserNameByToken(Request.Headers["Authorization"].ToString().Split(" ")[1]);
            return Ok(await _orderService.GetByIdByUsernameAsync(id, user));
        }
    }
}
