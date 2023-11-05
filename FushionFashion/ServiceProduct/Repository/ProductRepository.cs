using System.Threading.Tasks;
using System;
using System.Linq;
using BusinessObject.Enum.EnumStatus;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities.Product;
using ServiceProduct.Services;
using ServiceProduct.IRepository;
using ServiceProduct.IServices;

namespace ServiceProduct.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DBContext _appDBContext;
        private ICurrentTime _timeService;
        private IClaimService _claimService;

        public ProductRepository(DBContext appDBContext, ICurrentTime currentTime,
            IClaimService claimService) : base(appDBContext, currentTime, claimService)
        {
            _appDBContext = appDBContext;
            _timeService = currentTime;
            _claimService = claimService;
        }

        public Task<Product> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetEnableProduct()
        {
            var enableProduct = await _appDBContext.Products
       .Where(p => p.Status == EnumStatus.Enable)
       .FirstOrDefaultAsync();

            return enableProduct;
        }

        public async Task<Product> GetDisableProduct()
        {
            var disableProduct = await _appDBContext.Products
         .Where(p => p.Status == EnumStatus.Disable)
         .FirstOrDefaultAsync();

            return disableProduct;

        }

        public async Task<Product> GetProductByName(string name)
        {
            var product = await _appDBContext.Products
        .Where(p => p.Name == name)
        .FirstOrDefaultAsync();

            return product;
        }
    }
}

