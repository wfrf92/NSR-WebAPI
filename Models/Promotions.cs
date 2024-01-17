// Models/Promotion.cs

public class Promotion
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Condition { get; set; }
    public string Description { get; set; }
    public string LargeDescription { get; set; }
    public string ImageUrl { get; set; }
    public string Discount { get; set; }
    public bool Active { get; set; }
}
