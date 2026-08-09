using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.IRepository
{
    public interface ICustomerQueryRepository
    {
        Task<Customer?> GetCustomerAsync(Guid customerId);
        Task<List<Customer>> GetAllCustomersAsync();
    }
}
