using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ProductGUI.Models;

namespace ProductGUI.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7211/"); // Promeni port ako je drugačiji
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("api/products");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ProductDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}