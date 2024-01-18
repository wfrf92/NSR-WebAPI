// Controllers/CategoryController.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

[Authorize]
[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult GetAllCategories()
    {
        var categories = _categoryService.GetAllCategories();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public IActionResult GetCategoryById(int id)
    {
        var category = _categoryService.GetCategoryById(id);

        if (category != null)
        {
            return Ok(category);
        }

        return NotFound(new { message = "Category not found" });
    }

    [HttpPost]
    public IActionResult AddCategory([FromBody] Category newCategory)
    {
        _categoryService.AddCategory(newCategory);
        return Ok(new { message = "Category added successfully" });
    }

    [HttpPut]
    public IActionResult UpdateCategory([FromBody] Category updatedCategory)
    {
        _categoryService.UpdateCategory(updatedCategory);
        return Ok(new { message = "Category updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        _categoryService.DeleteCategory(id);
        return Ok(new { message = "Category deleted successfully" });
    }
}
