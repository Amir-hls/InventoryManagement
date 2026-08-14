using Application.IServices;
using Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using Application.DTOs.Customer;
using Application.IRepository;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerCommandRepository _customerCommand;
        private readonly ICustomerQueryRepository _customerQuery;
        public CustomerService(ICustomerCommandRepository customerCommand,
            ICustomerQueryRepository customerQueryRepository)
        {
            _customerCommand = customerCommand;
            _customerQuery = customerQueryRepository;
        }
        public async Task<Guid> InsertCustomer(AddCustomerDto addCustomerDto)
        {
            Customer newCustomer = addCustomerDto.Adapt<Customer>();
            await _customerCommand.InsertCustomer(newCustomer);
            return newCustomer.Id;
        }

        public async Task<bool> DeleteCustomer(Guid customerId)
        {
            bool isDeleted = await _customerCommand.DeleteCustomer(customerId);
            return isDeleted;
        }

        public async Task<CustomerDto?> GetCustomerAsync(Guid customerId)
        {
            var existingCustomer = await _customerQuery.GetCustomerAsync(customerId);
            if (existingCustomer == null) return null;
            return existingCustomer.Adapt<CustomerDto>();
        }

        public async Task<IReadOnlyList<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerQuery.GetAllCustomersAsync();

            return customers.Adapt<IReadOnlyList<CustomerDto>>();
        }

        public async Task<bool> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            if(updateCustomerDto.Id == Guid.Empty)
                throw new ArgumentException("Customer id cannot be empty",
                    nameof(updateCustomerDto));
            Customer? customer = await _customerCommand.GetCustomerEntityAsync(updateCustomerDto.Id);
            if (customer == null) return false;

            await _customerCommand.UpdateCustomer(customer);
            return true;
        }
    }
}
