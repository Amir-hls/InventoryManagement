using Application.DTOs.Product;
using Application.DTOs.WarehouseStock;
using Application.IRepository;
using Application.IServices;
using Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ProductService(IProductCommandRepository commandRepository,
        IProductQueryRepository queryRepository) : IProductService
    {
        public async Task<bool> DeleteProduct(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentException("Product id can not be null");
            bool isDeleted = await commandRepository.DeleteProduct(productId);
            return isDeleted;

        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            IEnumerable<Product> products = await queryRepository.GetProductsAsync();
            return products.Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name,
                UnitPrice = p.UnitPrice,
                Sku = p.Sku,
                StockDtos = p.WarehouseStocks.Select(st => new StockDto()
                {
                    StockQuantity = st.StockQuantity,
                    WarehouseName = st.Warehouse.Name,
                    WarehouseAddress = st.Warehouse.Address
                })
            });
        }

        public async Task<Guid> InsertProduct(AddProductDto addProductDto)
        {
            Product product = addProductDto.Adapt<Product>();
            return await commandRepository.InsertProduct(product);
        }

        public async Task<bool> UpdateProduct(UpdateProductDto updateProductDto)
        {
            if (updateProductDto.Id == Guid.Empty)
                throw new ArgumentException("Product Id can't be empty", nameof(updateProductDto));
            Product? existingProduct = await commandRepository.GetProductEntityAsync(updateProductDto.Id);
            if (existingProduct == null) return false;
            await commandRepository.UpdateProduct(existingProduct);
            return true;
        }
    }
}
