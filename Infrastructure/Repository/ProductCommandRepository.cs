using Application.IRepository;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class ProductCommandRepository(AppDbContext dbContext) : IProductCommandRepository
    {

        public async Task<bool> DeleteProduct(Guid productId)
        {
            Product? existingProduct = await GetProductEntityAsync(productId);
            if (existingProduct == null) return false;
            dbContext.Remove(existingProduct);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Product?> GetProductEntityAsync(Guid productId)
        {
            Product? existingProduct = await dbContext.FindAsync<Product>(productId);
            return existingProduct;
        }

        public async Task<Guid> InsertProduct(Product product)
        {
            await dbContext.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task UpdateProduct(Product product)
        {
            dbContext.Update(product);
            await dbContext.SaveChangesAsync();
        }
    }
}
