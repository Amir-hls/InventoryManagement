using Application.IRepository;
using Dapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class ProductQueryRepository(ISqlConnectionFactory connectionFactory) : IProductQueryRepository
    {
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            string sql = @"Select p.id, p.name,p.sku,p.unit_price,
                ws.id,ws.stock_quantity,ws.warehouse_id,
                w.id , w.name
                from products p 
                left join warehouse_stock ws on ws.product_id = p.id
                left join warehouse w on w.id = ws.warehouse_id; 
                ";
            Dictionary<Guid, Product> products = new Dictionary<Guid, Product>();
            using var connection = connectionFactory.CreateConnection();
            await connection.QueryAsync<Product,
                WarehouseStock, Warehouse, Product>(sql,(product,warehouseStock,warehouse) =>
                {
                    if(!products.TryGetValue(product.Id,out var existingProduct))
                    {
                        existingProduct = product;
                        products.Add(existingProduct.Id, existingProduct);
                    }
                    if(warehouseStock.Id != 0)
                    {
                        if (warehouse.Id != Guid.Empty)
                        {
                            warehouseStock.Warehouse = warehouse ;

                        }
                        existingProduct.WarehouseStocks.Add(warehouseStock);
 
                    }
                    return existingProduct;

                }
                ,splitOn: "id"
                );
            return products.Values;

        }
    }
}
