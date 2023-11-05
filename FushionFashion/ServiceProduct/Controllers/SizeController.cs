using BusinessObject.Dtos.Product;
using BusinessObject.Dtos.ProductInfo;
using BusinessObject.Dtos.Size;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceProduct.IServices;
using ServiceProduct.Services;
using System;
using System.Threading.Tasks;

namespace ServiceProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;
        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet("GetAllSize")]
        public async Task<ActionResult<SizeViewModel>> GetAllSize()
        {
            var product = await _sizeService.GetSize();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        [HttpPost("CreateSize")]
        public async Task<ActionResult<CreateSizeViewModel>> CreateSize(CreateSizeViewModel sizeViewModel)
        {
            if (ModelState.IsValid)
            {

                await _sizeService.CreateSize(sizeViewModel);

                return Ok("Created a new size successfully.");

            }

            return BadRequest("Invalid input or validation failed.");
        }

        [HttpPut("UpdateSize/{id}")]
        public async Task<ActionResult<UpdateSizeViewModel>> UpdateSize(Guid id, UpdateSizeViewModel updateDTO)
        {
            if (ModelState.IsValid)
            {
                var updatedSize = await _sizeService.UpdateSize(id, updateDTO);

                if (updatedSize != null)
                {
                    return Ok("Update Size Success");
                }
                else
                {
                    return NotFound("Product with the specified ID not found");
                }
            }
            return BadRequest("Invalid Model State");
        }

        [HttpDelete("DeleteSize")]
        public async Task DeleteSize(Guid id) => await _sizeService.DeleteSize(id);
    }
}
