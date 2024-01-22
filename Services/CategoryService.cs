// Services/CategoryService.cs

using System.Collections.Generic;
using Newtonsoft.Json;

public class CategoryService : ICategoryService
{
    private List<Category> _categories;

    public CategoryService()
    {
        _categories = GetCategoryFromJsonFile();
    }

    public List<Category> GetCategoryFromJsonFile()
    {
        var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Json/categories.json");

        if (!File.Exists(jsonFilePath))
        {
            // Handle the case when the file does not exist
            return new List<Category>();
        }

        var json = File.ReadAllText(jsonFilePath);
        var products = JsonConvert.DeserializeObject<List<Category>>(json);

        return products ?? new List<Category>();
    }

    public List<Category> GetAllCategories()
    {
        return _categories;
    }

    public Category GetCategoryById(int id)
    {
        return _categories.Find(category => category.Id == id);
    }

    public void AddCategory(Category newCategory)
    {
        _categories.Add(newCategory);
    }

    public void UpdateCategory(Category updatedCategory)
    {
        var index = _categories.FindIndex(category => category.Id == updatedCategory.Id);
        if (index != -1)
        {
            _categories[index] = updatedCategory;
        }
    }

    public void DeleteCategory(int id)
    {
        _categories.RemoveAll(category => category.Id == id);
    }
}
