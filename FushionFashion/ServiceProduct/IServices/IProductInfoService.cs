using BusinessObject.Dtos.Category;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BusinessObject.Dtos.ProductInfo;
using BusinessObject.Dtos.Account;

namespace ServiceProduct.IServices
{
    public interface IProductInfoService
    {
        Task<List<ProductInfoViewModel>> GetProductInfo();
        public Task<CreateProductInfoViewModel?> CreateProductInfo(CreateProductInfoViewModel proinfoDTO);
        Task DeleteProductInfo(Guid id);
        public Task<UpdateProductInfoViewModel?> UpdateProductInfo(Guid id, UpdateProductInfoViewModel proinfoDTO);
        Task<List<ProductInfoViewModel>?> GetProductInfoByProduct(Guid productId);

    }
}
//public Task<ProductInfoViewModel> GetProductInfoByProduct(Guid ProductId);