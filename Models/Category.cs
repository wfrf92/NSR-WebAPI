// Models/Category.cs

using System.Collections.Generic;

public class Subcategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string ImageUrl { get; set; }
    public List<Subcategory> Subcategories { get; set; }
    public bool Active { get; set; }
}
