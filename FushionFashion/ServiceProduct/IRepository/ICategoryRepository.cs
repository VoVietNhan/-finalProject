using System.Threading.Tasks;
using System;
using BusinessObject.Entities.Product;

namespace ServiceProduct.IRepository
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task<Category> GetCategoryByName(string name);
    }
}
