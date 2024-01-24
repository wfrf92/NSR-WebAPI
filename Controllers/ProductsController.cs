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

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImages([FromForm] ImageUploadModel imageUploadModel)
    {
        try
        {
            // Instantiate the product variable
            Product product = new Product();
             string azureAppServiceUrl = Url.ActionContext.HttpContext.Request.Scheme + "://" + Url.ActionContext.HttpContext.Request.Host.Value;
           
            string webAppPath = AppDomain.CurrentDomain.BaseDirectory;
            // Save the primary image
            string primaryImageFileName =
                $"{Guid.NewGuid().ToString()}_{imageUploadModel.PrimaryImage.FileName}";
            string primaryImagePath = Path.Combine(webAppPath,"Images/", primaryImageFileName);

            using (var primaryImageFileStream = new FileStream(primaryImagePath, FileMode.Create))
            {
                await imageUploadModel.PrimaryImage.CopyToAsync(primaryImageFileStream);
            }

            // Set the ImagePath property in the Product model for the main image
            product.PrimaryImage = azureAppServiceUrl + "/Images/"+ primaryImageFileName;

            // Save other images
            List<string> otherImagePaths = new List<string>();

            foreach (var otherImage in imageUploadModel.OtherImages)
            {
                // Save the primary image
                string otherImageFileName = $"{Guid.NewGuid().ToString()}_{otherImage.FileName}";
               
                string otherImagePath = Path.Combine(webAppPath,"Images/", otherImageFileName);

                using (var otherImageFileStream = new FileStream(otherImagePath, FileMode.Create))
                {
                    await otherImage.CopyToAsync(otherImageFileStream);
                }

                otherImagePaths.Add(azureAppServiceUrl + "/Images/"+ otherImageFileName);
            }

            // Set the OtherImages property in the Product model
            product.OtherImages = otherImagePaths
                .Select(path => new OtherImage { Image = path })
                .ToList();

            return Ok(product);
        }
        catch (Exception ex)
        {
            // Handle exceptions and return an appropriate response
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [Authorize]
    [HttpPost("createProduct")]
    public async Task<IActionResult> CreateProduct([FromBody] Product product)
    {
        var createdProduct = await _productService.CreateProductAsync(product);
        return CreatedAtAction(
            nameof(CreateProduct),
            new { id = createdProduct.Id },
            createdProduct
        );
    }

    [HttpPost("createQuotation")]
    public async Task<IActionResult> CreateQuotation([FromBody] Quotation quotation)
    {
        var createdProduct = await _productService.CreateQuotationAsync(quotation);
        return CreatedAtAction(
            nameof(CreateQuotation),
            new { id = createdProduct.Id },
            createdProduct
        );
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
