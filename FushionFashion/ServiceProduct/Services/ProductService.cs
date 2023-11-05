using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BusinessObject.Entities.Product;
using BusinessObject.Dtos.Product;
using ServiceProduct.Repository;
using Microsoft.EntityFrameworkCore;
using ServiceProduct.IServices;

namespace ServiceProduct.Services
{
    public class ProductService:  IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
        }


        public async Task<CreateProductViewModel?> CreateProduct(CreateProductViewModel productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _unitOfWork.ProductRepository.AddAsync(product);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<CreateProductViewModel>(product);
            }
            return null;
        }

        public Task DeleteProduct(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductViewModel> GetDisableProduct()
        {
            var disabledProducts = await _unitOfWork.ProductRepository.GetDisableProduct();
            if (disabledProducts == null)
            {
                return null;
            }
            return _mapper.Map<ProductViewModel>(disabledProducts);
        }



        public async Task<ProductViewModel> GetEnableProduct()
        {
            var product = await _unitOfWork.ProductRepository.GetEnableProduct();

            if (product == null)
            {
                return null;
            }

            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<List<ProductViewModel>> GetProduct()
        {
            var product = await _unitOfWork.ProductRepository.GetAllAsync();
            return _mapper.Map<List<ProductViewModel>>(product);
        }

        public async Task<ProductViewModel> GetProductById(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<ProductViewModel> GetProductByName(string name)
        {
            var product = await _unitOfWork.ProductRepository.GetProductByName(name);

            if (product == null)
            {
                return null;
            }

            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task<UpdateProductViewModel?> UpdateProduct(Guid id, UpdateProductViewModel productDTO)
        {

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product == null)
            {
                return null;
            }
            _mapper.Map(productDTO, product);

            _unitOfWork.ProductRepository.Update(product);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

            if (isSuccess)
            {
                return _mapper.Map<UpdateProductViewModel>(product);
            }

            return null;
        }

    }
}
