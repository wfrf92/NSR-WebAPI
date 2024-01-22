// Models/Quotation.cs

using Newtonsoft.Json;

public class Quotation
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("quotationNumber")]
    public string QuotationNumber { get; set; }

    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("customername")]
    public string CustomerName { get; set; }

    [JsonProperty("customeraddress")]
    public string CustomerAddress { get; set; }

    [JsonProperty("customerEmailAddress")]
    public string CustomerEmailAddress { get; set; }

    [JsonProperty("customerContactNo")]
    public long CustomerContactNo { get; set; }

    [JsonProperty("note")]
    public string Note { get; set; }

    [JsonProperty("productName")]
    public string ProductName { get; set; }

    [JsonProperty("productDescription")]
    public string ProductDescription { get; set; }

    [JsonProperty("active")]
    public bool Active { get; set; }
}
