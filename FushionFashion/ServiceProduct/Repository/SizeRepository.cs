using BusinessObject.Entities.Product;
using ServiceProduct.IRepository;
using ServiceProduct.IServices;

namespace ServiceProduct.Repository
{
    public class SizeRepository : GenericRepository<Size>, ISizeRepository
    {
        private readonly DBContext _appDBContext;
        private ICurrentTime _timeService;
        private IClaimService _claimService;

        public SizeRepository(DBContext appDBContext, ICurrentTime currentTime,
            IClaimService claimService) : base(appDBContext, currentTime, claimService)
        {
            _appDBContext = appDBContext;
            _timeService = currentTime;
            _claimService = claimService;
        }
    }
}
