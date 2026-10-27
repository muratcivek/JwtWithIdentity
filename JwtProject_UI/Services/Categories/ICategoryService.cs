using JwtProject_UI.DTOs.Categories;

namespace JwtProject_UI.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllCategoriesAsync();
    }
}
