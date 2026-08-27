using Application.IRepository;
using Application.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class StockQuantityService(IStockQuantityRepository stockQuantityRepository,
        IProductCommandRepository productCommand ) : IStockQuantityService
    {
        public async Task<bool> DecrementStockQuantity(Guid productId, int requestedQuantity)
        {
            if (productId == Guid.Empty) 
                throw new ArgumentNullException("product id can not be null");
            if (requestedQuantity <= 0)
                throw new ArgumentOutOfRangeException("invalid quantity");

            Product? product = await productCommand.GetProductEntityAsync(productId);
            if (product == null)
                throw new Exception("Invalid Product");
            return await stockQuantityRepository.DecrementProductQuantity(productId, requestedQuantity);
        }
    }
}
