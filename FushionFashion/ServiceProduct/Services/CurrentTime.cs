using ServiceProduct.IServices;
using System;

namespace ServiceProduct.Services
{
    public class CurrentTime : ICurrentTime
    {
        DateTime ICurrentTime.CurrentTime() => DateTime.UtcNow.AddHours(7);
    }
}
