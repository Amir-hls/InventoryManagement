using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IRepository
{
    public interface IProductQueryRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
