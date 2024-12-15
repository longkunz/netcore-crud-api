using Netcore.CRUDAPI.Dtos;

namespace Netcore.CRUDAPI.Services;

public interface ICategoryService
{
	Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
	Task<CategoryDto?> GetCategoryByIdAsync(int id);
	Task<CategoryDto> CreateCategoryAsync(CategoryDto category);
	Task UpdateCategoryAsync(int id, CategoryDto category);
	Task DeleteCategoryAsync(int id);
}
