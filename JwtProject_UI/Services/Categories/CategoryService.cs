using JwtProject_UI.DTOs.Categories;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace JwtProject_UI.Services.Categories
{
    public class CategoryService(
        HttpClient _client,
        IHttpContextAccessor _httpContextAccessor) : ICategoryService
    {
        public async Task<List<ResultCategoryDto>> GetAllCategoriesAsync()
        {
            var response = await _client.GetFromJsonAsync<List<ResultCategoryDto>>("categories");
            return response;
        }
    }
}