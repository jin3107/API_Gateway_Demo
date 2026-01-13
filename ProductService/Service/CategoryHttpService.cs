
using System.Text.Json;

namespace ProductService.Service
{
    public class CategoryHttpService : ICategoryHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CategoryHttpService> _logger;

        public CategoryHttpService(HttpClient httpClient, ILogger<CategoryHttpService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string?> GetCategoryNameByIdAsync(Guid categoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/categories/{categoryId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var jsonDoc = JsonDocument.Parse(content);
                    if (jsonDoc.RootElement.TryGetProperty("name", out var nameElement))
                        return nameElement.GetString();
                    else
                        _logger.LogWarning($"Failed to get category {categoryId}. Status: {response.StatusCode}");
                }
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error calling CategoryService for category {categoryId}");
            }
            return null;
        }
    }
}
