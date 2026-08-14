using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IRepository
{
    public interface ICustomerQueryRepository
    {
        Task<Customer?> GetCustomerAsync(Guid customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
