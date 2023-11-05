
using System.Threading.Tasks;
using System;
using BusinessObject.Entities.Product;
using ServiceProduct.Repository;

namespace ServiceProduct.IRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetEnableProduct();
        Task<Product> GetDisableProduct();
        Task<Product> FindAsync(Guid id);
        Task<Product> GetById(Guid id);
        Task<Product> GetProductByName(string name);
    }
}
