using Microsoft.EntityFrameworkCore;
using Netcore.CRUDAPI.Database;
using Netcore.CRUDAPI.Models;

namespace Netcore.CRUDAPI.Repositories;

public class ShippingRepository(IApplicationDbContext context) : IRepository<Shipping>
{
	public async Task<IEnumerable<Shipping>> GetAllAsync()
	{
		return await context.Shippings.ToListAsync();
	}

	public async Task<Shipping?> GetByIdAsync(int id)
	{
		return await context.Shippings.FindAsync(id);
	}

	public async Task AddAsync(Shipping shipping)
	{
		context.Shippings.Add(shipping);
		await context.SaveChangesAsync();
	}

	public async Task UpdateAsync(Shipping shipping)
	{
		context.Shippings.Update(shipping);
		await context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var shipping = await context.Shippings.FindAsync(id);
		if (shipping != null)
		{
			context.Shippings.Remove(shipping);
			await context.SaveChangesAsync();
		}
	}
}
