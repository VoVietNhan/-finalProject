using System;

namespace ServiceCart.Services
{
    public interface IClaimService
    {
        public Guid GetCurrentUserId { get; }
    }
}
