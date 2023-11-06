using BusinessObject.Entities.Product;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using BusinessObject.Dtos.ProductInfo;

namespace ServiceProduct.IRepository
{
    public interface IProductInfoRepository : IGenericRepository<ProductInfo>
    {
        Task<ProductInfo> GetProductInfoById(Guid Id);
        Task<List<ProductInfo>> GetProductInfoByProduct(Guid ProductId);
        Task<ProductInfo> GetProductInfoByProductIdAndSizeId(Guid productId, Guid sizeId);
    }
}
