using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [Authorize]
    [HttpPost("createProduct")]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        var createdProduct = await _productService.CreateProductAsync(product);
        return CreatedAtAction(nameof(CreateProduct), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPost("createQuotation")]
    public async Task<IActionResult> CreateQuotation([FromBody] Quotation quotation)
    {
        var createdProduct = await _productService.CreateQuotationAsync(quotation);
        return CreatedAtAction(nameof(CreateQuotation), new { id = createdProduct.Id }, createdProduct);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();

        // Update properties based on your needs
        product.Name = updatedProduct.Name;
        // Update other properties

        await _productService.UpdateProductAsync(product);

        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();

        await _productService.DeleteProductAsync(product);

        return NoContent();
    }
}
