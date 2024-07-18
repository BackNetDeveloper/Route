using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OMS.Core.Entities;
using OMS.Core.IRepositories;
using RouteTechAhmedAtwanTask.Consts;
using Swashbuckle.AspNetCore.Annotations;
namespace RouteTechAhmedAtwanTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CustomerController(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new customer")]
        [SwaggerResponse(201, "Customer created successfully")]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            await _customerRepository.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerOrders), new { customerId = customer.CustomerId }, customer);
        }

        [HttpGet("{customerId}/orders")]
        [Authorize(Policy = SystemPolicy.CustomerPolicy)]
        [SwaggerOperation(Summary = "Get all orders for a customer")]
        [SwaggerResponse(200, "Returns the list of orders")]
        [SwaggerResponse(404, "Customer not found")]
        public async Task<IActionResult> GetCustomerOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }
    }
}
