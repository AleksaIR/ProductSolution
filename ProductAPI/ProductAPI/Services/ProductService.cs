using ProductAPI.DTO;
using ProductModels;
using ProductModels.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllProductsAsync();
            return products
                .Where(p => !string.IsNullOrEmpty(p.ProductName) && p.Price > 0)
                .Select(p => new ProductDto
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    CategoryID = p.CategoryID,
                    CategoryName = p.Category?.CategoryName,
                    SupplierID = p.SupplierID,
                    SupplierName = p.Supplier?.SupplierName
                }).ToList();
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetProductByIdAsync(id);
            if (product == null) return null;
            return new ProductDto
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Price = product.Price,
                CategoryID = product.CategoryID,
                CategoryName = product.Category?.CategoryName,
                SupplierID = product.SupplierID,
                SupplierName = product.Supplier?.SupplierName
            };
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            if (productDto == null) return null;
            var product = new Product
            {
                ProductName = productDto.ProductName,
                Price = productDto.Price,
                CategoryID = productDto.CategoryID,
                SupplierID = productDto.SupplierID
            };
            await _unitOfWork.Products.AddProductAsync(product);
            await _unitOfWork.SaveChangesAsync();
            productDto.ProductID = product.ProductID;
            return productDto;
        }

        public async Task UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _unitOfWork.Products.GetProductByIdAsync(id);
            if (product == null) return;
            product.ProductName = productDto.ProductName;
            product.Price = productDto.Price;
            product.CategoryID = productDto.CategoryID;
            product.SupplierID = productDto.SupplierID;
            await _unitOfWork.Products.UpdateProductAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            await _unitOfWork.Products.DeleteProductAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<ProductDto>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await _unitOfWork.Products.GetProductsByCategoryAsync(categoryId);
            return products
                .Where(p => !string.IsNullOrEmpty(p.ProductName) && p.Price > 0)
                .Select(p => new ProductDto
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    CategoryID = p.CategoryID,
                    CategoryName = p.Category?.CategoryName,
                    SupplierID = p.SupplierID,
                    SupplierName = p.Supplier?.SupplierName
                }).ToList();
        }

        public async Task<List<ProductDto>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var products = await _unitOfWork.Products.GetAllProductsAsync();
            return products
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice && !string.IsNullOrEmpty(p.ProductName) && p.Price > 0)
                .Select(p => new ProductDto
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    CategoryID = p.CategoryID,
                    CategoryName = p.Category?.CategoryName,
                    SupplierID = p.SupplierID,
                    SupplierName = p.Supplier?.SupplierName
                }).ToList();
        }
    }
}