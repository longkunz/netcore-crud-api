using Netcore.CRUDAPI.Models;
using Netcore.CRUDAPI.Repositories;

namespace Netcore.CRUDAPI.Database
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<Category> CategoryRepository { get; }
		IRepository<Product> ProductRepository { get; }
		IRepository<Order> OrderRepository { get; }
		IRepository<Shipping> ShippingRepository { get; }
		IRepository<OutboxMessage> OutboxMessageRepository { get; }
		Task<int> SaveChangesAsync();
	}
}
