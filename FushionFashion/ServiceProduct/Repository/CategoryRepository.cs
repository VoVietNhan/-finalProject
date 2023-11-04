using BusinessObject.Entities.Product;
using ServiceProduct.IRepository;
using ServiceProduct.IServices;
using System.Threading.Tasks;
using System;

namespace ServiceProduct.Repository
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository  
    {
        private readonly DBContext _appDBContext;
        private ICurrentTime _timeService;
        private IClaimService _claimService;

        public CategoryRepository(DBContext appDBContext, ICurrentTime currentTime,
            IClaimService claimService) : base(appDBContext, currentTime, claimService)
        {
            _appDBContext = appDBContext;
            _timeService = currentTime;
            _claimService = claimService;
        }

      
    }
}
