using Domain.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface ICustomerService
    {
        Task AddCustomer(AddCustomerDto addCustomerDto);
        Task UpdateCustomer(UpdateCustomerDto updateCustomerDto);
        Task DeleteCustomer(Guid customerId);
        Task<CustomerDto?> GetCustomerAsync(Guid customerId);
        Task<List<CustomerDto>> GetAllCustomersAsync();

    }
}
