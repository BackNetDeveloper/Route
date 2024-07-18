using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Core.Entities;
using OMS.Core.IServices;
using RouteTechAhmedAtwanTask.Consts;
using RouteTechAhmedAtwanTask.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace RouteTechAhmedAtwanTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Policy = SystemPolicy.CustomerPolicy)]
        [SwaggerOperation(Summary = "Create a new order")]
        [SwaggerResponse(201, "Order created successfully")]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            var createdOrder = await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { orderId = createdOrder.OrderId }, createdOrder);
        }

        [HttpGet("{orderId}")]
        [Authorize(Policy = SystemPolicy.CustomerPolicy)]
        [SwaggerOperation(Summary = "Get details of a specific order")]
        [SwaggerResponse(200, "Returns the order details")]
        [SwaggerResponse(404, "Order not found")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderAsync(orderId); 
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpGet]
        [Authorize(Policy = SystemPolicy.AdminPolicy)]
        [SwaggerOperation(Summary = "Get all orders (admin only)")]
        [SwaggerResponse(200, "Returns the list of orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpPut("{orderId}/status")]
        [Authorize(Policy = SystemPolicy.AdminPolicy)]
        [SwaggerOperation(Summary = "Update order status (admin only)")]
        [SwaggerResponse(200, "Order status updated successfully")]
        [SwaggerResponse(404, "Order not found")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatusUpdateDto statusUpdateDto)
        {
            var updatedOrder = await _orderService.UpdateOrderStatusAsync(orderId, statusUpdateDto.Status);
            if (updatedOrder == null)
            {
                return NotFound();
            }
            return Ok(updatedOrder);
        }
    }
}
