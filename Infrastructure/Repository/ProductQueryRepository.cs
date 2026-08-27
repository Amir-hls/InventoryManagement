using Application.DTOs.Product;
using Application.DTOs.WarehouseStock;
using Application.IRepository;
using Dapper;
using Domain.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class ProductQueryRepository(ISqlConnectionFactory connectionFactory) : IProductQueryRepository
    {
        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            string sql = @"Select p.id, p.name,p.sku,p.unit_price,
                ws.stock_quantity,
                w.name as warehouse_name
                from products p 
                left join warehouse_stock ws on ws.product_id = p.id
                left join warehouse w on w.id = ws.warehouse_id; 
                ";
            Dictionary<Guid, ProductDto> products = new Dictionary<Guid, ProductDto>();
            using var connection = connectionFactory.CreateConnection();
            await connection.QueryAsync<ProductDto,
                StockDto, ProductDto>(sql,(product,stockDto) =>
                {
                    if(!products.TryGetValue(product.Id,out var existingProduct))
                    {
                        existingProduct = product;
                        existingProduct.StockDtos = new();
                        products.Add(existingProduct.Id, existingProduct);
                    }
                    if(stockDto != null && 
                    !string.IsNullOrWhiteSpace(stockDto.WarehouseName))
                    {
                        existingProduct.StockDtos?.Add(stockDto);
 
                    }
                    return existingProduct;

                }
                ,splitOn: "stock_quantity"
                );
            return products.Values;

        }
        public async Task<ProductDto?> GetProductByProductIdAsync(Guid productId)
        {
            string sql = @"Select p.id, p.name,p.sku,p.unit_price from products p
                where p.id = @ProductId;
                
                Select ws.stock_quantity,
                w.name as warehouse_name
                from warehouse_stock ws 
                inner join warehouse w on w.id = ws.warehouse_id
                where ws.product_id = @ProductId;
                ";
            using var connection = connectionFactory.CreateConnection();
            Dictionary<Guid, Product> products = new();
            using var multi = await connection.QueryMultipleAsync(sql, 
                new { ProductId = productId });
            var product = await multi.ReadSingleOrDefaultAsync<ProductDto>();
            if (product == null) return null;
            var stocks = await multi.ReadAsync<StockDto>();
            if (stocks.Count() == 0) return product;
            product.StockDtos = stocks.ToList();
            return product;

        }
    }
}
