using AutoMapper;
using Netcore.CRUDAPI.Database;
using Netcore.CRUDAPI.Dtos;
using Netcore.CRUDAPI.Models;

namespace Netcore.CRUDAPI.Services;

public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
	public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
	{
		var categories = await unitOfWork.CategoryRepository.GetAllAsync();
		return mapper.Map<IEnumerable<CategoryDto>>(categories);
	}

	public async Task<CategoryDto> GetCategoryByIdAsync(int id)
	{
		var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
		return mapper.Map<CategoryDto>(category);
	}

	public async Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto)
	{
		var category = mapper.Map<Category>(categoryDto);
		await unitOfWork.CategoryRepository.AddAsync(category);
		await unitOfWork.SaveChangesAsync();
		return mapper.Map<CategoryDto>(category);
	}

	public async Task UpdateCategoryAsync(int id, CategoryDto categoryDto)
	{
		if (id != categoryDto.Id)
		{
			throw new ArgumentException("Id không khớp với Category.");
		}

		var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
		if (category == null)
		{
			throw new ArgumentException("Không tìm thấy Category.");
		}

		mapper.Map(categoryDto, category);

		await unitOfWork.CategoryRepository.UpdateAsync(category);
		await unitOfWork.SaveChangesAsync();
	}

	public async Task DeleteCategoryAsync(int id)
	{
		await unitOfWork.CategoryRepository.DeleteAsync(id);
		await unitOfWork.SaveChangesAsync();
	}
}
