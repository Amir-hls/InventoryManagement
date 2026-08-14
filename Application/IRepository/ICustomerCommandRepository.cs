using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IRepository
{
    public interface ICustomerCommandRepository
    {
        Task<Guid> InsertCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task<bool> DeleteCustomer(Guid customerId);
        Task<Customer?> GetCustomerEntityAsync(Guid customerId);

    }
}
