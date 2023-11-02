using System;

namespace ServiceCart.Services
{
    public class ClaimService : IClaimService
    {
        public Guid GetCurrentUserId { get; }
    }
}
