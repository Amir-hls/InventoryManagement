using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface IStockQuantityService
    {
        Task<bool> DecrementStockQuantity(Guid productId, int requestedQuantity);
    }
}
