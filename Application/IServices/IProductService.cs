using Application.DTOs.Product;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface IProductService
    {
        Task<Guid> InsertProduct(AddProductDto addProductDto);
        Task<bool> UpdateProduct(UpdateProductDto updateProductDto);
        Task<bool> DeleteProduct(Guid productId);
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<ProductDto?> GetProductByProductId(Guid productId);

    }
}
