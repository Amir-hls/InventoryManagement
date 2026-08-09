using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.IRepository
{
    public interface ICustomerCommandRepository
    {
        Task InsertCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(Guid customerId);
    }
}
