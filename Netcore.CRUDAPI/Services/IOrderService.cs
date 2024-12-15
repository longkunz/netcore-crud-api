using Netcore.CRUDAPI.Dtos;

namespace Netcore.CRUDAPI.Services;

public interface IOrderService
{
	Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
	Task<OrderDto> GetOrderByIdAsync(int id);
	Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
	Task UpdateOrderAsync(int id, OrderDto orderDto);
	Task DeleteOrderAsync(int id);
}
