using ServiceProduct.IRepository;
using System.Threading.Tasks;

namespace ServiceProduct
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }  
        IProductInfoRepository ProductInfoRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
