using Microsoft.AspNetCore.Mvc;
using OrderApi.Models;
using OrderApi.Services;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var orders = await _service.GetAllAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                Console.WriteLine(ex);
                return StatusCode(500, ex);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _service.GetByIdAsync(id);
            return order == null ? NotFound() : Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var created = await _service.CreateAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Order order)
        {
            if (id != order.Id) return BadRequest();
            await _service.UpdateAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedOrders(
            int pageNumber = 1,
            int pageSize = 10,
            string sortColumn = "Id",
            string sortDirection = "ASC",
            string? labelNo = null,
            string? carrier = null,
            string? contactName = null)
        {
            var result = await _service.GetOrdersPagedAsync(
                pageNumber, pageSize,
                sortColumn, sortDirection,
                labelNo, carrier, contactName);

            return Ok(result);
        }
    }
}