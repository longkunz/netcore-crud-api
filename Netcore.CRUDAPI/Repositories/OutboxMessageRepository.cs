using Microsoft.EntityFrameworkCore;
using Netcore.CRUDAPI.Database;
using Netcore.CRUDAPI.Models;

namespace Netcore.CRUDAPI.Repositories;

public class OutboxMessageRepository(IApplicationDbContext context) : IRepository<OutboxMessage>
{
	public async Task<IEnumerable<OutboxMessage>> GetAllAsync()
	{
		return await context.OutboxMessages.ToListAsync();
	}

	public async Task<IEnumerable<OutboxMessage>> GetAllAsync(System.Linq.Expressions.Expression<Func<OutboxMessage, bool>> predicate)
	{
		return await context.OutboxMessages.Where(predicate).ToListAsync();
	}

	public async Task<OutboxMessage?> GetByIdAsync(int id)
	{
		return await context.OutboxMessages.FindAsync(id);
	}

	public async Task AddAsync(OutboxMessage outboxMessage)
	{
		context.OutboxMessages.Add(outboxMessage);
		await context.SaveChangesAsync();
	}

	public async Task UpdateAsync(OutboxMessage outboxMessage)
	{
		context.OutboxMessages.Update(outboxMessage);
		await context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var outboxMessage = await context.OutboxMessages.FindAsync(id);
		if (outboxMessage != null)
		{
			context.OutboxMessages.Remove(outboxMessage);
			await context.SaveChangesAsync();
		}
	}
}
