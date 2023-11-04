using AutoMapper;
using BusinessObject.Dtos.Category;
using BusinessObject.Dtos.ProductInfo;
using BusinessObject.Entities.Product;
using BusinessObject.Enum.EnumStatus;
using ServiceProduct.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceProduct.Services
{
    public class ProductInfoService : IProductInfoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;

        public ProductInfoService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
        }
        public async Task<CreateProductInfoViewModel> CreateProductInfo(CreateProductInfoViewModel proinfoDTO)
        {
            var proinfo = _mapper.Map<Category>(proinfoDTO);
            proinfoDTO.Status = EnumStatus.Enable;
            await _unitOfWork.CategoryRepository.AddAsync(proinfo);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<CreateProductInfoViewModel>(proinfo);
            }
            return null;
        }

        public async Task DeleteProductInfo(Guid id)
        {
            var proinfo = await _unitOfWork.ProductInfoRepository.GetByIdAsync(id);
            proinfo.Status = EnumStatus.Enable;
            _unitOfWork.ProductInfoRepository.Update(proinfo);
            _unitOfWork.SaveChangeAsync();

        }

        public Task<List<ProductInfoViewModel>> GetProductInfo()
        {
            throw new NotImplementedException();
        }

        public Task<UpdateProductInfoViewModel> UpdateProductInfo(Guid id, UpdateProductInfoViewModel proinfoDTO)
        {
            throw new NotImplementedException();
        }
    }
}
