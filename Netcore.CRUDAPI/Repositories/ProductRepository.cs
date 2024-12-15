using Microsoft.EntityFrameworkCore;
using Netcore.CRUDAPI.Database;
using Netcore.CRUDAPI.Models;

namespace Netcore.CRUDAPI.Repositories
{
	public class ProductRepository(IApplicationDbContext context) : IRepository<Product>
	{
		public async Task AddAsync(Product entity)
		{
			context.Products.Add(entity);
			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var product = await context.Products.FindAsync(id);
			if (product != null)
			{
				context.Products.Remove(product);
				await context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await context.Products.ToListAsync();
		}

		public async Task<Product?> GetByIdAsync(int id)
		{
			return await context.Products.FindAsync(id);
		}

		public async Task UpdateAsync(Product entity)
		{
			context.Entry(entity).State = EntityState.Modified;
			await context.SaveChangesAsync();
		}
	}
}
