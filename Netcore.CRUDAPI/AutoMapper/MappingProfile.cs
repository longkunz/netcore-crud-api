using AutoMapper;
using Netcore.CRUDAPI.Dtos;
using Netcore.CRUDAPI.Models;

namespace Netcore.CRUDAPI.AutoMapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Category, CategoryDto>().ReverseMap();
			CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<Order, OrderDto>().ReverseMap();
			CreateMap<Shipping, ShippingDto>().ReverseMap();
			CreateMap<OrderItem, OrderItemDto>().ReverseMap();
		}
	}
}
