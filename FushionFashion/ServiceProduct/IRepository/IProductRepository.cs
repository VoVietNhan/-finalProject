
using System.Threading.Tasks;
using System;
using BusinessObject.Entities.Product;

namespace ServiceProduct.IRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetEnableProduct();
        Task<Product> GetDisableProduct();
        Task<Product> FindAsync(Guid id);
    }
}
