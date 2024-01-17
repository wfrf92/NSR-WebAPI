// Models/Quotation.cs

public class Quotation
{
    public int Id { get; set; }
    public string QuotationNumber { get; set; }
    public DateTime Date { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerEmailAddress { get; set; }
    public long CustomerContactNo { get; set; }
    public string Note { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public bool Active { get; set; }
}
