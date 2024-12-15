using Netcore.CRUDAPI.Database;
using Netcore.CRUDAPI.Dtos;
using Newtonsoft.Json;

namespace Netcore.CRUDAPI.Services;

public class OutboxProcessor(IServiceProvider serviceProvider) : IHostedService, IDisposable
{
	private Timer _timer = null!;

	public Task StartAsync(CancellationToken stoppingToken)
	{
		_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5)); // Chạy mỗi 5 giây
		return Task.CompletedTask;
	}

	private async void DoWork(object? state)
	{
		using var scope = serviceProvider.CreateScope();
		var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
		var shippingService = scope.ServiceProvider.GetRequiredService<IShippingService>();

		// 1. Lấy các message "CreateShipping" chưa được xử lý từ bảng Outbox
		var messages = (await unitOfWork.OutboxMessageRepository.GetAllAsync()).Where(m => !m.Processed && m.Type == "CreateShipping");

		foreach (var message in messages)
		{
			try
			{
				// 2. Deserialize Data thành ShippingDto
				var shippingDto = JsonConvert.DeserializeObject<ShippingDto>(message.Data);
				if (shippingDto != null)
				{
					// 3. Gọi ShippingService để tạo Shipping
					await shippingService.CreateShippingAsync(shippingDto);

					// 4. Đánh dấu message là đã xử lý
					message.Processed = true;
					await unitOfWork.OutboxMessageRepository.UpdateAsync(message);
				}
			}
			catch (Exception ex)
			{
				// Xử lý exception (ví dụ: log lỗi, retry, ...)
				Console.WriteLine($"Error processing outbox message: {ex.Message}");
			}
		}

		await unitOfWork.SaveChangesAsync();
	}

	public Task StopAsync(CancellationToken stoppingToken)
	{
		_timer?.Change(Timeout.Infinite, 0);
		return Task.CompletedTask;
	}

	public void Dispose()
	{
		_timer?.Dispose();
	}
}
