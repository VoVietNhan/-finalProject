using System.Threading.Tasks;
using System;
using ServiceProduct.IRepository;

namespace ServiceProduct
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext _appDBContext;
        private readonly IProductRepository _productRepository;
        private  readonly ICategoryRepository _categoryRepository;
        private readonly IProductInfoRepository _productInfoRepository;
        private readonly ISizeRepository _sizeRepository;

        public UnitOfWork(DBContext appDBContext,
            IProductRepository productRepository, 
            ICategoryRepository categoryRepository,
            IProductInfoRepository productInfoRepository,
            ISizeRepository sizeRepository)
        {
            _appDBContext = appDBContext;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productInfoRepository = productInfoRepository;
            _sizeRepository = sizeRepository;
        }
        public IProductRepository ProductRepository => _productRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IProductInfoRepository ProductInfoRepository => _productInfoRepository;  
        public ISizeRepository SizeRepository => _sizeRepository;
        public async Task<int> SaveChangeAsync() => await _appDBContext.SaveChangesAsync();
    }
}
