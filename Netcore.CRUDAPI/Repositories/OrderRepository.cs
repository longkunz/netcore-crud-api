using Microsoft.EntityFrameworkCore;
using Netcore.CRUDAPI.Database;
using Netcore.CRUDAPI.Models;

namespace Netcore.CRUDAPI.Repositories;

public class OrderRepository(IApplicationDbContext context) : IRepository<Order>
{
	public async Task<IEnumerable<Order>> GetAllAsync()
	{
		return await context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.ToListAsync();
	}

	public async Task<Order?> GetByIdAsync(int id)
	{
		return await context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(oi => oi.Product)
			.FirstOrDefaultAsync(o => o.Id == id);
	}

	public async Task AddAsync(Order order)
	{
		context.Orders.Add(order);
		await context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Order order)
	{
		context.Orders.Update(order);
		await context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var order = await context.Orders.FindAsync(id);
		if (order != null)
		{
			context.Orders.Remove(order);
			await context.SaveChangesAsync();
		}
	}
}
