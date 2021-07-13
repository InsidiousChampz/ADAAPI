using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerProFileAPI.DTOs.Order;
using CustomerProFileAPI.Services.Order;

namespace CustomerProFileAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _orderService;

        public OrdersController(IOrdersService orderService)
        {
            _orderService = orderService;
        }

        //Order
        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrder()
        {
            return Ok(await _orderService.GetOrder());
        }

        [HttpGet("orders/{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            return Ok(await _orderService.GetOrderById(orderId));
        }

        [HttpGet("orders/filter")]
        public async Task<IActionResult> GetOrderWithFilter([FromQuery] GetOrderFilterDto filter)
        {
            return Ok(await _orderService.GetOrderWithFilter(filter));
        }

        [HttpGet("orders/filterbydate")]
        public async Task<IActionResult> GetOrderWithFilterByDate([FromQuery] GetOrderDateFilterDTO filter)
        {
            return Ok(await _orderService.GetOrderWithFilterByDate(filter));
        }


        [HttpPost("addorder")]
        public async Task<IActionResult> AddOrder(AddOrderDto newOrder)
        {
            return Ok(await _orderService.AddOrder(newOrder));
        }
        //Order list
        //TODO: Get order list

        //Order and Order list
        [HttpPost("addbothorders")]
        public async Task<IActionResult> AddOrders(AddBothOrderDto newOrder)
        {
            return Ok(await _orderService.AddBothOrder(newOrder));
        }
        [HttpPut("updateorderstatusbyid/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatusById(int orderId, UpdateOrderStatusDto updateOrder)
        {
            return Ok(await _orderService.UpdateOrderStatusById(orderId, updateOrder));
        }
    }
}