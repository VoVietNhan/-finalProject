using BusinessObject.Entities.Product;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ServiceProduct.IRepository
{
    public interface IProductInfoRepository : IGenericRepository<ProductInfo>
    {
        Task<ProductInfo> GetProductInfoById(Guid Id);
        Task<List<ProductInfo>> GetProductInfoByProduct(Guid ProductId);
    }
}
