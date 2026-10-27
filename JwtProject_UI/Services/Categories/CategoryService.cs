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
            var token = _httpContextAccessor.HttpContext?
                .User.FindFirst("Token")?.Value;

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new UnauthorizedAccessException(
                    "JWT token bulunamadı. Lütfen tekrar giriş yapın.");
            }

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                "categories");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            using var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<List<ResultCategoryDto>>()
                ?? new List<ResultCategoryDto>();
        }
    }
}