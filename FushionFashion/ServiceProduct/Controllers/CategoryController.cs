using BusinessObject.Dtos.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceProduct.IServices;
using System.Threading.Tasks;
using System;

namespace ServiceProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("GetAllcategory")]
        public async Task<ActionResult<CategoryViewModel>> GetAllCategory()
        {
            var product = await _categoryService.GetCategory();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<CategoryViewModel>> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
        [HttpPost("CreateCategory")]
        public async Task<ActionResult<CreateCategoryViewModel>> CreateProduct(CreateCategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {

                await _categoryService.CreateCategory(categoryViewModel);

                return Ok("Created a new category successfully.");

            }

            return BadRequest("Invalid input or validation failed.");
        }
        [HttpPut("UpdateCategory/{id}")]
        public async Task<ActionResult<UpdateCategoryViewModel>> UpdateProduct(Guid id, UpdateCategoryViewModel categoryDTO)
        {
            if (ModelState.IsValid)
            {
                var updatedCategory = await _categoryService.UpdateCategory(id, categoryDTO);

                if (updatedCategory != null)
                {
                    return Ok("Update Category Success");
                }
                else
                {
                    return NotFound("Category with the specified ID not found");
                }
            }
            return BadRequest("Invalid Model State");
        }
        [HttpDelete("DeleteCategory")]
        public async Task DeleteProduct(Guid id) => await _categoryService.DeleteCategory(id);
    }
}
