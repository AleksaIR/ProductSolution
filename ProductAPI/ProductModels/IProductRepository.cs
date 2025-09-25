using ProductModels.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductModels
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<List<Product>> GetProductsByCategoryAsync(int categoryId); // Dodato
    }
}