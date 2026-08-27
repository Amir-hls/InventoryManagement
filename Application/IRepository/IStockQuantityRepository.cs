using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IRepository
{
    public interface IStockQuantityRepository
    {
        Task<bool> DecrementProductQuantity(Guid productId, int requestedQuantity);
    }
}
