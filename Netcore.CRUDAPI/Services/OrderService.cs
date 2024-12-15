using AutoMapper;
using Netcore.CRUDAPI.Database;
using Netcore.CRUDAPI.Dtos;
using Netcore.CRUDAPI.Models;
using Newtonsoft.Json;

namespace Netcore.CRUDAPI.Services;

public class OrderService(IUnitOfWork unitOfWork, IMapper mapper) : IOrderService
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IMapper _mapper = mapper;

	public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
	{
		var orders = await _unitOfWork.OrderRepository.GetAllAsync();
		return _mapper.Map<IEnumerable<OrderDto>>(orders);
	}

	public async Task<OrderDto> GetOrderByIdAsync(int id)
	{
		var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
		return _mapper.Map<OrderDto>(order);
	}

	public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
	{
		// 1. Tạo Order
		var order = _mapper.Map<Order>(orderDto);
		order.OrderDate = DateTime.Now;

		// 2. Tính toán TotalAmount (lấy từ OrderItems)
		order.TotalAmount = order.OrderItems.Sum(item => item.Price * item.Quantity);

		// 3. Lưu Order và OrderItems vào database
		await _unitOfWork.OrderRepository.AddAsync(order);

		// 4. Tạo message cho Shipping (Outbox Pattern)
		var shippingDto = new ShippingDto { OrderId = order.Id, /* Khởi tạo thông tin Shipping */ };
		var message = new OutboxMessage
		{
			Type = "CreateShipping",
			Data = JsonConvert.SerializeObject(shippingDto),
			CreatedAt = DateTime.Now,
			Processed = false
		};
		await _unitOfWork.OutboxMessageRepository.AddAsync(message);
		await _unitOfWork.SaveChangesAsync();

		return _mapper.Map<OrderDto>(order);
	}

	public async Task UpdateOrderAsync(int id, OrderDto orderDto)
	{
		if (id != orderDto.Id)
		{
			throw new ArgumentException("Id không khớp với Order.");
		}

		var existingOrder = await _unitOfWork.OrderRepository.GetByIdAsync(id);
		if (existingOrder == null)
		{
			throw new ArgumentException("Không tìm thấy Order.");
		}

		_mapper.Map(orderDto, existingOrder);

		// Cập nhật TotalAmount (nếu cần)
		existingOrder.TotalAmount = existingOrder.OrderItems.Sum(item => item.Price * item.Quantity);

		await _unitOfWork.OrderRepository.UpdateAsync(existingOrder);
		await _unitOfWork.SaveChangesAsync();
	}

	public async Task DeleteOrderAsync(int id)
	{
		await _unitOfWork.OrderRepository.DeleteAsync(id);
		await _unitOfWork.SaveChangesAsync();
	}
}
