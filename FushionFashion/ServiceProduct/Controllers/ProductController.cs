using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using BusinessObject.Dtos.Product;
using ServiceProduct.Services;
using ServiceProduct.IServices;

namespace ServiceProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProduct")]
        public async Task<ActionResult<ProductViewModel>> GetAllProduct()
        {
            var product = await _productService.GetProduct();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProductById(Guid id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("CreateProduct")]
        public async Task<ActionResult<CreateProductViewModel>> CreateProduct(CreateProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {

                await _productService.CreateProduct(productViewModel);

                return Ok("Created a new product successfully.");

            }

            return BadRequest("Invalid input or validation failed.");
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<ActionResult<UpdateProductViewModel>> UpdateProduct([FromQuery] Guid id, UpdateProductViewModel productDTO)
        {
            if (ModelState.IsValid)
            {
                var updatedProduct = await _productService.UpdateProduct(id, productDTO);

                if (updatedProduct != null)
                {
                    return Ok("Update Product Success");
                }
                else
                {
                    return NotFound("Product with the specified ID not found");
                }
            }
            return BadRequest("Invalid Model State");
        }

        [HttpGet("enable/{id}")]
        public async Task<IActionResult> GetEnableProduct()
        {
            var productViewModel = await _productService.GetEnableProduct();

            if (productViewModel == null)
            {
                return BadRequest("No product found.");
            }

            return Ok(new { Message = "Search Succeed", Product = productViewModel });
        }

        [HttpGet("disable/{id}")]
        public async Task<IActionResult> GetDisableProducts()
        {
            var productViewModels = await _productService.GetDisableProduct();

            if (productViewModels == null)
            {
                return BadRequest("No disabled products found.");
            }

            return Ok(productViewModels);
        }

        [HttpGet("GetProductByName/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var productViewModel = await _productService.GetProductByName(name);

            if (productViewModel == null)
            {
                return BadRequest("No product found with the specified name.");
            }

            return Ok(productViewModel);
        }
    }
}
