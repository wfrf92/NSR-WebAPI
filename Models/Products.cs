using Newtonsoft.Json;

public class Product
{
    public int Id { get; set; }

    [JsonProperty("product_id")]
    public int ProductId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    [JsonProperty("subdescription")]
    public string Subdescription { get; set; }

    [JsonProperty("main_feature")]
    public string MainFeature { get; set; }

    public Category Category { get; set; }
    public Subcategory Subcategory { get; set; }

    [JsonProperty("manufacturer")]
    public string Manufacturer { get; set; }

    [JsonProperty("primary_image")]
    public string PrimaryImage { get; set; }

    [JsonProperty("other_images")]
    public List<OtherImage> OtherImages { get; set; }

    public string Specifications { get; set; }
    public string Features { get; set; }
    public string Other { get; set; }
    public Promotion Promotions { get; set; }
    public string Condition { get; set; }
    public bool Active { get; set; }
}
