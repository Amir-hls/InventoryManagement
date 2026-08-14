using Application.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface ICustomerService
    {
        Task<Guid> InsertCustomer(AddCustomerDto addCustomerDto);
        Task<bool> UpdateCustomer(UpdateCustomerDto updateCustomerDto);
        Task<bool> DeleteCustomer(Guid customerId);
        Task<CustomerDto?> GetCustomerAsync(Guid customerId);
        Task<IReadOnlyList<CustomerDto>> GetAllCustomersAsync();

    }
}
