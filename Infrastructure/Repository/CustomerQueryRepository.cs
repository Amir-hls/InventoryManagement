using Dapper;
using Domain.Entities;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Repository
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        public CustomerQueryRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            string sql = @"Select c.Id,c.Name,c.PhoneNumber,c.CompanyName,
c.ContactEmail,c.TaxNumber, o.Id, o.OrderNumber, o.OrderDate, os.Id,
os.Code From Customers c Left JOIN Orders o On o.CustomerId = c.Id
LEFT JOIN OrderStatus os on os.Id = o.OrderStatusId";
            var customerDictionary = new Dictionary<Guid, Customer>();

            using IDbConnection connection = _connectionFactory.CreateConnection();
            await connection.QueryAsync<Customer,Order,
                OrderStatus,Customer>(
                sql,
                (customer,order,orderStatus) =>
                {
                    if(!customerDictionary.TryGetValue(customer.Id,
                        out var currentCustomer))
                    {
                        currentCustomer = customer;
                        currentCustomer.Orders = new List<Order>();
                        customerDictionary.Add(currentCustomer.Id, currentCustomer);
                    }
                    if(order != null)
                    {
                        if(orderStatus != null)
                        {
                            order.OrderStatus = orderStatus;
                        }
                        currentCustomer.Orders.Add(order);
                    }
                    return currentCustomer;
                },
                splitOn: "Id"
                );
            return customerDictionary.Values.AsList();

        }

        public async Task<Customer?> GetCustomerAsync(Guid customerId)
        {
            string sql = @"Select c.Id,c.Name,c.PhoneNumber,c.CompanyName,
c.ContactEmail,c.TaxNumber, o.Id, o.OrderNumber, o.OrderDate, os.Id,
os.Code From Customers c Left JOIN Orders o On o.CustomerId = c.Id
LEFT JOIN OrderStatus os on os.Id = o.OrderStatusId WHERE c.Id = @CustomerId";
            var customerDictionary = new Dictionary<Guid, Customer>();
            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.
                QueryAsync<Customer,Order,OrderStatus,Customer>
                (sql,
                (customer,order,orderStatus) =>
                {
                    if(!customerDictionary.TryGetValue(customer.Id,
                        out var currentCustomer))
                    {
                        currentCustomer = customer;
                        currentCustomer.Orders = new List<Order>();
                    }
                    if(order != null)
                    {
                        if(orderStatus != null)
                        {
                            order.OrderStatus = orderStatus;
                        }
                        currentCustomer.Orders.Add(order);

                    }
                    return currentCustomer;
                },
                new {CustomerId = customerId },
                splitOn:"Id"
                );
            return customerDictionary.Values.FirstOrDefault();
        }
    }
}
