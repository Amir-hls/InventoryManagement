using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IRepository
{
    public interface IProductCommandRepository
    {
        Task<Guid> InsertProduct(Product product);
        Task UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid productId);
        Task<Product?> GetProductAsync(Guid productId);

    }
}
