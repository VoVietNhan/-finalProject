using BusinessObject.Dtos.Category;
using BusinessObject.Dtos.Product;
using BusinessObject.Dtos.ProductInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceProduct.IRepository;
using ServiceProduct.IServices;
using ServiceProduct.Services;
using System;
using System.Threading.Tasks;

namespace ServiceProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInfoController : ControllerBase
    {
        private readonly IProductInfoService _productInfoRepository;
        public ProductInfoController(IProductInfoService productInfoService)
        {
            _productInfoRepository = productInfoService;
        }

        [HttpGet("GetAllcategory")]
        public async Task<ActionResult<ProductInfoViewModel>> GetAllInfo()
        {
            var product = await _productInfoRepository.GetProductInfo();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("CreateProductInfo")]
        public async Task<ActionResult<CreateProductInfoViewModel>> CreateProduct(CreateProductInfoViewModel productInfoViewModel)
        {
            if (ModelState.IsValid)
            {

                await _productInfoRepository.CreateProductInfo(productInfoViewModel);

                return Ok("Created a new product successfully.");

            }

            return BadRequest("Invalid input or validation failed.");
        }

        [HttpPut("UpdateProductInfo/{id}")]
        public async Task<ActionResult<UpdateProductInfoViewModel>> UpdateProductInfo(Guid id, UpdateProductInfoViewModel productInfoDTO)
        {
            if (ModelState.IsValid)
            {
                var updatedProduct = await _productInfoRepository.UpdateProductInfo(id, productInfoDTO);

                if (updatedProduct != null)
                {
                    return Ok("Update ProductInfo Success");
                }
                else
                {
                    return NotFound("Product with the specified ID not found");
                }
            }
            return BadRequest("Invalid Model State");
        }

        [HttpDelete("DeleteProductInfo")]
        public async Task DeleteProduct(Guid id) => await _productInfoRepository.DeleteProductInfo(id);
    }
}
