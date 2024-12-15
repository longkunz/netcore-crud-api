using Microsoft.AspNetCore.Mvc;
using Netcore.CRUDAPI.Dtos;
using Netcore.CRUDAPI.Services;

namespace Netcore.CRUDAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
	private readonly IOrderService _orderService = orderService;

	// GET: api/Orders
	[HttpGet]
	public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
	{
		var orders = await _orderService.GetAllOrdersAsync(); // Implement phương thức này trong OrderService
		return Ok(orders);
	}

	// GET: api/Orders/5
	[HttpGet("{id}")]
	public async Task<ActionResult<OrderDto>> GetOrder(int id)
	{
		var order = await _orderService.GetOrderByIdAsync(id); // Implement phương thức này trong OrderService
		if (order == null)
		{
			return NotFound();
		}
		return Ok(order);
	}

	// POST: api/Orders
	[HttpPost]
	public async Task<ActionResult<OrderDto>> PostOrder(OrderDto orderDto)
	{
		var createdOrder = await _orderService.CreateOrderAsync(orderDto);
		return CreatedAtAction("GetOrder", new { id = createdOrder.Id }, createdOrder);
	}

	// PUT: api/Orders/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutOrder(int id, OrderDto orderDto)
	{
		if (id != orderDto.Id)
		{
			return BadRequest();
		}

		try
		{
			await _orderService.UpdateOrderAsync(id, orderDto); // Implement phương thức này trong OrderService
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}

		return NoContent();
	}

	// DELETE: api/Orders/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteOrder(int id)
	{
		await _orderService.DeleteOrderAsync(id); // Implement phương thức này trong OrderService
		return NoContent();
	}
}
