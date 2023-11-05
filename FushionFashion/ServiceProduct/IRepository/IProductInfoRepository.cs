using BusinessObject.Entities.Product;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ServiceProduct.IRepository
{
    public interface IProductInfoRepository : IGenericRepository<ProductInfo>
    {
        Task<ProductInfo> GetProductInfoByProduct(Guid ProductId);
    }
}
