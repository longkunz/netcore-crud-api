using Microsoft.AspNetCore.Mvc;
using Netcore.CRUDAPI.Dtos;
using Netcore.CRUDAPI.Models;
using Netcore.CRUDAPI.Services;

namespace Netcore.CRUDAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
	private readonly IProductService _productService = productService;

	// GET: api/Products
	[HttpGet]
	public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
	{
		return Ok(await _productService.GetAllProductsAsync());
	}

	// GET: api/Products/5
	[HttpGet("{id}")]
	public async Task<ActionResult<ProductDto>> GetProduct(int id)
	{
		var product = await _productService.GetProductByIdAsync(id);
		if (product == null)
		{
			return NotFound();
		}
		return product;
	}

	// POST: api/Products
	[HttpPost]
	public async Task<ActionResult<ProductDto>> PostProduct(ProductDto product)
	{
		var createdProduct = await _productService.CreateProductAsync(product);
		return CreatedAtAction("GetProduct", new { id = createdProduct.Id }, createdProduct);
	}

	// PUT: api/Products/5
	[HttpPut("{id}")]
	public async Task<IActionResult> PutProduct(int id, ProductDto product)
	{
		try
		{
			await _productService.UpdateProductAsync(id, product);
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		return NoContent();
	}

	// DELETE: api/Products/5
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteProduct(int id)
	{
		await _productService.DeleteProductAsync(id);
		return NoContent();
	}
}
