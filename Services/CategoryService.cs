using System.Text.Json;

namespace blazorappdemo;

public class CategoryService(HttpClient httpClient): ICategoryService
{
    private readonly HttpClient client = httpClient;
    private readonly JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    public async Task<List<Category>?> GetCategoriesAsync()
    {
        var response = await client.GetAsync("v1/categories");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<List<Category>>(content, options);
    }
}

public interface ICategoryService
{
    Task<List<Category>?> GetCategoriesAsync();
}