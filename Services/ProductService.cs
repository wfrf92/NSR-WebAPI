using Newtonsoft.Json;

public class ProductService : IProductService
{
    private List<Product> products = new List<Product>();

    public ProductService()
    {
        products = GetProductsFromJsonFile();
    }

    public List<Product> GetProductsFromJsonFile()
    {
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Json/products.json");

        if (!File.Exists(jsonFilePath))
        {
            // Handle the case when the file does not exist
            return new List<Product>();
        }

        var json = File.ReadAllText(jsonFilePath);
        var products = JsonConvert.DeserializeObject<List<Product>>(json);

        return products ?? new List<Product>();
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await Task.FromResult(products);
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await Task.FromResult(products.FirstOrDefault(p => p.Id == id));
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        product.Id = products.Count + 1;
        products.Add(product);

        // Serialize the updated products list to JSON
        string json = JsonConvert.SerializeObject(products);

        // Specify the path to your JSON file
        string filePath = "Json/products.json";

        // Write the JSON data to the file
        await File.WriteAllTextAsync(filePath, json);

        return await Task.FromResult(product);
    }

    public async Task UpdateProductAsync(Product updatedProduct)
    {
        var existingProduct = products.FirstOrDefault(p => p.Id == updatedProduct.Id);
        if (existingProduct != null)
        {
            // Update properties based on your needs
            existingProduct.Name = updatedProduct.Name;
            // Update other properties
        }

        await Task.CompletedTask;
    }

    public async Task DeleteProductAsync(Product product)
    {
        products.Remove(product);

        await Task.CompletedTask;
    }
}
