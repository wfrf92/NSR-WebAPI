public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task<Product> CreateProductAsync(Product product);
    Task<Quotation> CreateQuotationAsync(Quotation quotation);
}
