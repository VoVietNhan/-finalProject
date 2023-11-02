using ServiceProduct.IRepository;
using System.Threading.Tasks;
using System;

namespace ServiceProduct
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _appDBContext;
        private readonly IProductRepository _productRepository;

        public UnitOfWork(DBContext appDBContext,
            IProductRepository productRepository)
        {
            _appDBContext = appDBContext;
            _productRepository = productRepository;
        }
        public IProductRepository ProductRepository => _productRepository;

        public async Task<int> SaveChangeAsync() => await _appDBContext.SaveChangesAsync();
    }
}
