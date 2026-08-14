using Application.IRepository;
using Dapper;
using Domain.Entities;
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
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            string sql = @"
    SELECT c.id, c.name, c.phone_number, c.company_name, 
           c.contact_email, c.tax_number, 
           o.id, o.order_number, o.order_date, 
           os.id, os.code 
    FROM customers c 
    LEFT JOIN orders o ON o.customer_id = c.id
    LEFT JOIN order_status os ON os.id = o.order_status_id";
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
                splitOn: "id"
                );
            return customerDictionary.Values;

        }

        public async Task<Customer?> GetCustomerAsync(Guid customerId)
        {
            string sql = @"SELECT c.id, c.name, c.phone_number, c.company_name, 
           c.contact_email, c.tax_number, 
           o.id, o.order_number, o.order_date, 
           os.id, os.code 
    FROM customers c 
    LEFT JOIN orders o ON o.customer_id = c.id
    LEFT JOIN order_status os ON os.id = o.order_status_id WHERE c.id = @CustomerId";
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
                splitOn:"id"
                );
            return customerDictionary.Values.FirstOrDefault();
        }
    }
}
