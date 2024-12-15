using Netcore.CRUDAPI.Models;
using Netcore.CRUDAPI.Repositories;

namespace Netcore.CRUDAPI.Database
{
	public class UnitOfWork(IApplicationDbContext context,
						  IRepository<Category> categoryRepository,
						  IRepository<Product> productRepository) : IUnitOfWork
	{
		private readonly IApplicationDbContext _context = context;
		public IRepository<Category> CategoryRepository { get; } = categoryRepository;
		public IRepository<Product> ProductRepository { get; } = productRepository;

		public void Dispose()
		{
			_context.Dispose();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
