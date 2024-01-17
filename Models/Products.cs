public class Product
{
   public int Id { get; set; }
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Subdescription { get; set; }
    public string? MainFeature { get; set; }
    public Category? Category { get; set; }
    public Category? Subcategory { get; set; }
    public string? Manufacturer { get; set; }
    public string? PrimaryImage { get; set; }
    public List<Image>? OtherImages { get; set; }
    public string? Specifications { get; set; }
    public string? Features { get; set; }
    public string? Other { get; set; }
    public Promotion? Promotions { get; set; }
    public string? Condition { get; set; }
    public bool Active { get; set; }
    // Add other properties based on the JSON structure
}
