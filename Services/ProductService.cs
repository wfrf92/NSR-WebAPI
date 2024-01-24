using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class ProductService : IProductService
{
    private List<Product> products = new List<Product>();
    private List<Quotation> quotations = new List<Quotation>();
    private IQuotationService quotationService;

    public ProductService()
    {
        products = GetProductsFromJsonFile();
        quotationService = new QuotationService();
        quotations = quotationService.GetAllQuotations();
    }

    public List<Product> GetProductsFromJsonFile()
    {
        var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Json/products.json");

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
        product.ProductId = product.Id;
        product.Active = true;

        products.Add(product);
        // Serialize the updated products list to JSON
        string json = JsonConvert.SerializeObject(products);

        // Specify the path to your JSON file
        string jfilePath = "Json/products.json";

        // Write the JSON data to the file
        await File.WriteAllTextAsync(jfilePath, json);

        return await Task.FromResult(product);
    }

    public async Task<Quotation> CreateQuotationAsync(Quotation quotation)
    {
        quotation.Id = quotations.Count + 1;
        quotation.QuotationNumber =
            "QN" + DateTime.Now.ToString("yyyy") + quotation.Id.ToString("00000");
        quotation.Date = DateTime.Now;
        quotations.Add(quotation);

        // Serialize the updated products list to JSON
        string json = JsonConvert.SerializeObject(quotations);

        // Specify the path to your JSON file
        string filePath = "Json/quotations.json";

        // Write the JSON data to the file
        await File.WriteAllTextAsync(filePath, json);

        // Send email
        await SendQuotationEmailAsync(quotation);

        return await Task.FromResult(quotation);
    }

    private async Task SendQuotationEmailAsync(Quotation quotation)
    {
        var appSettings = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            .GetSection("EmailConfiguration");

        string smtpServer = appSettings["SmtpServer"];
        int smtpPort = int.Parse(appSettings["SmtpPort"]);
        string smtpUsername = appSettings["SmtpUsername"];
        string smtpPassword = appSettings["SmtpPassword"];
        string senderEmail = appSettings["SenderEmail"];
        string toEmail = appSettings["ToEmail"];

        // Create a new instance of SmtpClient
        using (var client = new SmtpClient(smtpServer, smtpPort))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            client.EnableSsl = true;

            // Create a new MailMessage
            // Create a new MailMessage
            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = "Automated: New Quotation Request Received",
                Body =
                    $"Dear Nova Sports and Recreation Team,\n\n"
                    + $"This is an automated notification to inform you that a new quotation request has been received through our system. Please find the details below:\n\n"
                    + $"- Date: {quotation.Date.ToString("yyyy-MM-dd HH:mm:ss")}\n"
                    + $"- Quotation No: {quotation.QuotationNumber}\n"
                    + $"- Customer: {quotation.CustomerName}\n"
                    + $"- Product: {quotation.ProductName}\n"
                    + $"- Contact Email: {quotation.CustomerEmailAddress}\n"
                    + $"- Contact Number: {quotation.CustomerContactNo}\n"
                    + $"- Note: {quotation.Note}\n\n"
                    + $"---\n"
                    + $"This is an automated email from Nova Sports and Recreation."
            };

            // Add recipient email address
            mailMessage.To.Add(toEmail);

            try
            {
                // Send the email
                await client.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
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
