using Application.IRepository;
using Dapper;
using Domain.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Repository
{
    public class StockQuantityRepository(ISqlConnectionFactory connectionFactory) : IStockQuantityRepository
    {
        public async Task<bool> DecrementProductQuantity(Guid productId, int requestedQuantity)
        {
            using var connection = connectionFactory.CreateConnection();
            await connection.OpenAsync();
            using var transaction = await connection.
                BeginTransactionAsync(IsolationLevel.Serializable);
            int maxRetry = 5;
            int retryCount = 0;
            bool success = false;
            while(retryCount < maxRetry && !success)
            {
                try
                {
                    const string selectSql = @"Select id, warehouse_id, stock_quantity 
                    from warehouse_stock
                    where product_id = @ProductId
                    Order by warehouse_id asc For Update";
                    var stocks = (await connection.QueryAsync<WarehouseStock>(selectSql,
                        new { ProductId = productId }, transaction: transaction)).ToList();
                    int totalAvailable = stocks.Sum(s => s.StockQuantity);
                    if (totalAvailable < requestedQuantity)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                    int remainingToDeduct = requestedQuantity;
                    foreach (var stock in stocks)
                    {
                        if (remainingToDeduct == 0) break;

                        int deductFromThis = Math.Min(stock.StockQuantity, remainingToDeduct);
                        string updateSql = @"Update warehouse_stock 
                    Set stock_quantity = stock_quantity - @Deduct
                    Where id = @Id";
                        await connection.ExecuteAsync(updateSql,
                            new { Deduct = deductFromThis, Id = stock.Id },
                            transaction: transaction);

                        remainingToDeduct -= deductFromThis;
                    }
                    await transaction.CommitAsync();
                    success = true;
                }
                catch(PostgresException ex) when(ex.SqlState == "40001")
                {
                    await transaction.RollbackAsync();
                    retryCount++;
                    await Task.Delay(100 * retryCount);
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            return success;


        }
    }
}
