using Microsoft.EntityFrameworkCore;
using Netcore.CRUDAPI.Database;
using Netcore.CRUDAPI.Models;

namespace Netcore.CRUDAPI.Repositories
{
	public class CategoryRepository(IApplicationDbContext context) : IRepository<Category>
	{
		public async Task AddAsync(Category entity)
		{
			context.Categories.Add(entity);
			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var category = await context.Categories.FindAsync(id);
			if (category != null)
			{
				context.Categories.Remove(category);
				await context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Category>> GetAllAsync()
		{
			return await context.Categories.ToListAsync();
		}

		public async Task<Category?> GetByIdAsync(int id)
		{
			return await context.Categories.FindAsync(id);
		}

		public async Task UpdateAsync(Category entity)
		{
			context.Entry(entity).State = EntityState.Modified;
			await context.SaveChangesAsync();
		}
	}
}
