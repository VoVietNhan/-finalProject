using ServiceProduct.IRepository;
using System.Threading.Tasks;

namespace ServiceProduct
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }  
        public IProductInfoRepository ProductInfoRepository { get; }
        public ISizeRepository SizeRepository { get; }

        public Task<int> SaveChangeAsync();
    }
}
