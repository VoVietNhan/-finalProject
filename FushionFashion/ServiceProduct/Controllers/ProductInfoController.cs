using BusinessObject.Dtos.Category;
using BusinessObject.Dtos.Product;
using BusinessObject.Dtos.ProductInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceProduct.IRepository;
using ServiceProduct.IServices;
using ServiceProduct.Services;
using System;
using System.Collections.Generic;
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

        [HttpGet("GetAllProductInfo")]
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
        public async Task<ActionResult<CreateProductInfoViewModel?>> CreateProduct([FromBody]CreateProductInfoViewModel productInfoViewModel)
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


        [HttpGet("GetListProductInfoByProduct/{productId}")]
        public async Task<ActionResult<List<ProductInfoViewModel>>> GetListProductInfoByProduct(Guid productId)
        {
            var productInfo = await _productInfoRepository.GetListProductInfoByProduct(productId);

            if (productInfo == null)
            {
                return NotFound("Product with the specified ID not found");
            }

            return Ok(productInfo);
        }
        [HttpGet("GetProductInfoById/{Id}")]
        public async Task<ActionResult<ProductInfoViewModel>> GetProductInfoById(Guid Id)
        {
            var productInfo = await _productInfoRepository.GetProductInfoById(Id);

            if (productInfo == null)
            {
                return NotFound("Product with the specified ID not found");
            }

            return Ok(productInfo);
        }
/*        [HttpGet("GetProductInfoById/{productId}/{sizeId}")]
        public async Task<ActionResult<ProductInfoViewModel>> GetProductInfoByProductIdAndSizeId(Guid productId, Guid sizeId)
        {
            var productInfo = await _productInfoRepository.GetProductInfoByProductIdAndSizeId(productId, sizeId);

            if (productInfo == null)
            {
                return NotFound("Product with the specified ID not found");
            }

            return Ok(productInfo);
        }*/
    }
}
