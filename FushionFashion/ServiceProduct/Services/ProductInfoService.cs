using AutoMapper;
using BusinessObject.Dtos.Category;
using BusinessObject.Dtos.Product;
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
        public async Task<CreateProductInfoViewModel?> CreateProductInfo(CreateProductInfoViewModel proinfoDTO)
        {
            var proinfo = _mapper.Map<ProductInfo>(proinfoDTO);
            proinfoDTO.Status = EnumStatus.Enable;
            await _unitOfWork.ProductInfoRepository.AddAsync(proinfo);
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

        public async Task<List<ProductInfoViewModel>> GetProductInfo()
        {
            var category = await _unitOfWork.ProductInfoRepository.GetAllAsync();
            return _mapper.Map<List<ProductInfoViewModel>>(category);
        }

        public async Task<UpdateProductInfoViewModel> UpdateProductInfo(Guid id, UpdateProductInfoViewModel proinfoDTO)
        {
            var productInfo = await _unitOfWork.ProductInfoRepository.GetByIdAsync(id);

            if (productInfo == null)
            {
                return null;
            }
            _mapper.Map(proinfoDTO, productInfo);

            _unitOfWork.ProductInfoRepository.Update(productInfo);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

            if (isSuccess)
            {
                return _mapper.Map<UpdateProductInfoViewModel>(productInfo);
            }

            return null;
        }
    }
}
