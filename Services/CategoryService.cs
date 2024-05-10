using System.Text.Json;

namespace blazorappdemo;

public class CategoryService(HttpClient httpClient)
{
    private readonly HttpClient client = httpClient;

    public async Task<List<Category>?> GetCategoriesAsync()
    {
        var response = await client.GetStreamAsync("v1/categories");
        return await JsonSerializer.DeserializeAsync<List<Category>>(response);
    }
}