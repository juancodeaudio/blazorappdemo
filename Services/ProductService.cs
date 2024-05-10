using System.Net.Http.Json;
using System.Text.Json;

namespace blazorappdemo;

public class ProductService(HttpClient httpClient, JsonSerializerOptions optionsJson)
{
    private readonly HttpClient client = httpClient;
    private readonly JsonSerializerOptions options = optionsJson;

    public async Task<List<Product>?> GetProductsAsync()
    {
        var response = await client.GetStreamAsync("v1/products");
        return await JsonSerializer.DeserializeAsync<List<Product>>(response);
    }

    public async Task AddProductsAsync(Product product)
    {
        var response = await client.PostAsync("v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }

    public async Task DeleteProductAsync(int productId)
    {
        var response = await client.DeleteAsync($"v1/products/{productId}");
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException(content);
        }
    }
}