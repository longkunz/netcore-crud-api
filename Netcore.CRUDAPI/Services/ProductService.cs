using AutoMapper;
using Netcore.CRUDAPI.Database;
using Netcore.CRUDAPI.Dtos;
using Netcore.CRUDAPI.Models;

namespace Netcore.CRUDAPI.Services
{
	public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
	{
		public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
		{
			var products = await unitOfWork.ProductRepository.GetAllAsync();
			return mapper.Map<IEnumerable<ProductDto>>(products);
		}

		public async Task<ProductDto> GetProductByIdAsync(int id)
		{
			var product = await unitOfWork.ProductRepository.GetByIdAsync(id);
			return mapper.Map<ProductDto>(product);
		}

		public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
		{
			var product = mapper.Map<Product>(productDto);
			await unitOfWork.ProductRepository.AddAsync(product);
			await unitOfWork.SaveChangesAsync();
			return mapper.Map<ProductDto>(product);
		}

		public async Task UpdateProductAsync(int id, ProductDto productDto)
		{
			if (id != productDto.Id)
			{
				throw new ArgumentException("Id không khớp với Product.");
			}

			var product = await unitOfWork.ProductRepository.GetByIdAsync(id);
			if (product == null)
			{
				throw new ArgumentException("Không tìm thấy Product.");
			}

			mapper.Map(productDto, product);

			await unitOfWork.ProductRepository.UpdateAsync(product);
			await unitOfWork.SaveChangesAsync();
		}

		public async Task DeleteProductAsync(int id)
		{
			await unitOfWork.ProductRepository.DeleteAsync(id);
			await unitOfWork.SaveChangesAsync();
		}
	}
}
