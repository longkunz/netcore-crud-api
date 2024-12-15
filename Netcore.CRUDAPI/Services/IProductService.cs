using Netcore.CRUDAPI.Dtos;

namespace Netcore.CRUDAPI.Services
{
	public interface IProductService
	{
		Task<IEnumerable<ProductDto>> GetAllProductsAsync();
		Task<ProductDto?> GetProductByIdAsync(int id);
		Task<ProductDto> CreateProductAsync(ProductDto product);
		Task UpdateProductAsync(int id, ProductDto product);
		Task DeleteProductAsync(int id);
	}
}
