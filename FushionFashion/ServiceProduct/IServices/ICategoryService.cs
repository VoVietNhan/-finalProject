using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BusinessObject.Dtos.Category;

namespace ServiceProduct.IServices
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetCategory();
        public Task<CreateCategoryViewModel?> CreateCategory(CreateCategoryViewModel categoryDTO);
        Task DeleteCategory(Guid id);
        public Task<UpdateCategoryViewModel?> UpdateCategory(Guid id, UpdateCategoryViewModel categoryDTO);
        Task<CategoryViewModel> GetCategoryById(Guid id);
    }
}
