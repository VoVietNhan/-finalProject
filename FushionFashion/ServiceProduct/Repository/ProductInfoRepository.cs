using BusinessObject.Entities.Product;
using Microsoft.EntityFrameworkCore;
using ServiceProduct.IRepository;
using ServiceProduct.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceProduct.Repository
{
    public class ProductInfoRepository : GenericRepository<ProductInfo>, IProductInfoRepository
    {
        private readonly DBContext _appDBContext;
        private ICurrentTime _timeService;
        private IClaimService _claimService;

        public ProductInfoRepository(DBContext appDBContext, ICurrentTime currentTime,
            IClaimService claimService) : base(appDBContext, currentTime, claimService)
        {
            _appDBContext = appDBContext;
            _timeService = currentTime;
            _claimService = claimService;
        }

        public async Task<List<ProductInfo>> GetProductInfoByProduct(Guid ProductId)
        {
            var productInfo = await _appDBContext.ProductInfo.Where(x => x.ProductId.Equals(ProductId)).Include(c=>c.Size).ToListAsync();
                

            return productInfo;
        }
        public async Task<ProductInfo> GetProductInfoById(Guid id)
        {
            var productInfo = await _appDBContext.ProductInfo.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();


            return productInfo;
        }
    }
}
