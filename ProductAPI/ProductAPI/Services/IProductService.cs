using ProductAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductDto productDto);
        Task UpdateProductAsync(int id, ProductDto productDto);
        Task DeleteProductAsync(int id);
        Task<List<ProductDto>> GetProductsByCategoryAsync(int categoryId);
        Task<List<ProductDto>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }
}