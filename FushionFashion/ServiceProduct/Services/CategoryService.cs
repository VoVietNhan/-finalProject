using AutoMapper;
using BusinessObject.Dtos.Category;
using ServiceProduct.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BusinessObject.Entities.Product;
using BusinessObject.Enum.EnumStatus;

namespace ServiceProduct.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
        }

        public async Task<CreateCategoryViewModel?> CreateCategory(CreateCategoryViewModel categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            category.Status = EnumStatus.Enable;
            await _unitOfWork.CategoryRepository.AddAsync(category);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<CreateCategoryViewModel>(category);
            }
            return null;
        }


        public async Task<List<CategoryViewModel>> GetCategory()
        {
            var category = await _unitOfWork.CategoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryViewModel>>(category);
        }

        public async Task<CategoryViewModel> GetCategoryById(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task<CategoryViewModel> GetCategoryByName(string name)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategoryByName(name);

            if (category == null)
            {
                return null;
            }

            return _mapper.Map<CategoryViewModel>(category);
        }
    }
}
