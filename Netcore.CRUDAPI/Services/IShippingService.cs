using Netcore.CRUDAPI.Dtos;

namespace Netcore.CRUDAPI.Services;

public interface IShippingService
{
	Task CreateShippingAsync(ShippingDto shippingDto);
}
