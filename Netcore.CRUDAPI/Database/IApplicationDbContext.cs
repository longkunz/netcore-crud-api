using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Netcore.CRUDAPI.Models;

namespace Netcore.CRUDAPI.Database
{
	public interface IApplicationDbContext
	{
		DbSet<Category> Categories { get; set; }
		DbSet<Product> Products { get; set; }
		DbSet<Order> Orders { get; set; } // <--- NEW <--->
		DbSet<OrderItem> OrderItems { get; set; } // <--- NEW <--->
		DbSet<Shipping> Shippings { get; set; }
		DbSet<OutboxMessage> OutboxMessages { get; set; }
		void Dispose();
		EntityEntry Entry(object entity);
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
