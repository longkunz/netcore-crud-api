using AutoMapper;
using Netcore.CRUDAPI.Database;
using Netcore.CRUDAPI.Dtos;
using Netcore.CRUDAPI.Models;

namespace Netcore.CRUDAPI.Services;

public class ShippingService(IUnitOfWork unitOfWork, IMapper mapper) : IShippingService
{
	public async Task CreateShippingAsync(ShippingDto shippingDto)
	{
		// 1. Map ShippingDto sang Shipping
		var shipping = mapper.Map<Shipping>(shippingDto);

		// 2. Lưu Shipping vào database
		await unitOfWork.ShippingRepository.AddAsync(shipping);
		await unitOfWork.SaveChangesAsync();
	}
}
