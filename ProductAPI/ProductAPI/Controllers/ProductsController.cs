using Microsoft.AspNetCore.Mvc;
using ProductAPI.DTO;
using ProductAPI.Services;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("bycategory/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        [HttpGet("bypricerange")]
        public async Task<IActionResult> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var products = await _productService.GetProductsByPriceRangeAsync(minPrice, maxPrice);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdProduct = await _productService.CreateProductAsync(productDto);
            if (createdProduct == null)
            {
                return BadRequest("Failed to create product.");
            }
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.ProductID }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is invalid.");
            }
            await _productService.UpdateProductAsync(id, productDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}