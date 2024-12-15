using Netcore.CRUDAPI.Models;
using Netcore.CRUDAPI.Repositories;

namespace Netcore.CRUDAPI.Database
{
	public class UnitOfWork(IApplicationDbContext context,
						  IRepository<Category> categoryRepository,
						  IRepository<Product> productRepository,
						  IRepository<Order> orderRepository,
						  IRepository<Shipping> shippingRepository,
						  IRepository<OutboxMessage> outboxMessageRepository) : IUnitOfWork
	{
		private readonly IApplicationDbContext _context = context;
		public IRepository<Category> CategoryRepository { get; } = categoryRepository;
		public IRepository<Product> ProductRepository { get; } = productRepository;
		public IRepository<Order> OrderRepository { get; } = orderRepository;
		public IRepository<Shipping> ShippingRepository { get; } = shippingRepository;
		public IRepository<OutboxMessage> OutboxMessageRepository { get; } = outboxMessageRepository;

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
