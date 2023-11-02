using System;

namespace ServiceProduct.IServices
{
    public interface IClaimService
    {
        public Guid GetCurrentUserId { get; }
    }
}
