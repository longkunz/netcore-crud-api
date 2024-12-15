using Netcore.CRUDAPI.Models;
using Netcore.CRUDAPI.Repositories;

namespace Netcore.CRUDAPI.Database
{
	public interface IUnitOfWork : IDisposable
	{
		IRepository<Category> CategoryRepository { get; }
		IRepository<Product> ProductRepository { get; }
		Task<int> SaveChangesAsync();
	}
}
