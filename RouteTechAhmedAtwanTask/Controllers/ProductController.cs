using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.Core.Entities;
using OMS.Core.IRepositories;
using RouteTechAhmedAtwanTask.Consts;
using Swashbuckle.AspNetCore.Annotations;

namespace RouteTechAhmedAtwanTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all products")]
        [SwaggerResponse(200, "Returns the list of products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        [SwaggerOperation(Summary = "Get details of a specific product")]
        [SwaggerResponse(200, "Returns the product details")]
        [SwaggerResponse(404, "Product not found")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Policy = SystemPolicy.AdminPolicy)]
        [SwaggerOperation(Summary = "Add a new product (admin only)")]
        [SwaggerResponse(201, "Product created successfully")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            await _productRepository.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { productId = product.ProductId }, product);
        }

        [HttpPost("{productId}")]
        [Authorize(Policy = SystemPolicy.AdminPolicy)]
        [SwaggerOperation(Summary = "Update product details (admin only)")]
        [SwaggerResponse(200, "Product updated successfully")]
        [SwaggerResponse(404, "Product not found")]
        public async Task<IActionResult> UpdateProduct(int productId, Product product)
        {
            var updatedProduct = await _productRepository.UpdateProductAsync(productId, product);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return Ok(updatedProduct);
        }
    }
}
