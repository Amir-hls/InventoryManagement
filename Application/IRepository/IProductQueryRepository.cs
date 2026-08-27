using Application.DTOs.Product;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IRepository
{
    public interface IProductQueryRepository
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto?> GetProductByProductIdAsync(Guid productId);

    }
}
