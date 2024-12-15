using Microsoft.AspNetCore.Mvc;
using Netcore.CRUDAPI.Dtos;
using Netcore.CRUDAPI.Models;
using Netcore.CRUDAPI.Services;

namespace Netcore.CRUDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
	private readonly ICategoryService _categoryService = categoryService;

	// GET: api/Categories
	[HttpGet]
	public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
	{
		return Ok(await _categoryService.GetAllCategoriesAsync());
	}

	// GET: api/Categories/5
	[HttpGet("{id}")]
	public async Task<ActionResult<CategoryDto>> GetCategory(int id)
	{
		var category = await _categoryService.GetCategoryByIdAsync(id);
		if (category == null)
		{
			return NotFound();
		}
		return category;
	}

	// POST: api/Categories
	[HttpPost]
	public async Task<ActionResult<CategoryDto>> PostCategory(CategoryDto category)
	{
		var createdCategory = await _categoryService.CreateCategoryAsync(category);
		return CreatedAtAction("GetCategory", new { id = createdCategory.Id }, createdCategory);
	}

	// PUT: api/Categories/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutCategory(int id, CategoryDto category)
	{
		try
		{
			await _categoryService.UpdateCategoryAsync(id, category);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		return NoContent();
	}

	// DELETE: api/Categories/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteCategory(int id)
	{
		await _categoryService.DeleteCategoryAsync(id);
		return NoContent();
	}
}
