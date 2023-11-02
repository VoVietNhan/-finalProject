using ServiceProduct.IRepository;
using System.Threading.Tasks;

namespace ServiceProduct
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public Task<int> SaveChangeAsync();
    }
}
