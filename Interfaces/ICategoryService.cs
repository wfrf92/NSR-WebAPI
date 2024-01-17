// Interfaces/ICategoryService.cs

public interface ICategoryService
{
    List<Category> GetAllCategories();
    Category GetCategoryById(int id);
    void AddCategory(Category newCategory);
    void UpdateCategory(Category updatedCategory);
    void DeleteCategory(int id);
}
