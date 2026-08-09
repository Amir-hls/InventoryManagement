using Application.IServices;
using Domain.Entities;
using Mapster;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.DTOs.Customer;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerCommandRepository _customerCommand;
        private readonly ICustomerQueryRepository _customerQueryRepository;
        public CustomerService(ICustomerCommandRepository customerCommand,
            ICustomerQueryRepository customerQueryRepository)
        {
            _customerCommand = customerCommand;
            _customerQueryRepository = customerQueryRepository;
        }
        public async Task AddCustomer(AddCustomerDto addCustomerDto)
        {
            Customer newCustomer = addCustomerDto.Adapt<Customer>();
            await _customerCommand.InsertCustomer(newCustomer);
        }

        public async Task DeleteCustomer(Guid customerId)
        {
            await _customerCommand.DeleteCustomer(customerId);
        }

        public async Task<CustomerDto?> GetCustomerAsync(Guid customerId)
        {
            var existingCustomer = await _customerQueryRepository.GetCustomerAsync(customerId);
            if (existingCustomer == null) return null;
            return existingCustomer.Adapt<CustomerDto>();
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            List<Customer> customers = await _customerQueryRepository.GetAllCustomersAsync();
            if(customers.Count > 0)
            {
                return customers.Adapt<List<CustomerDto>>();
            }
            return new List<CustomerDto>();
        }

        public async Task UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            if(updateCustomerDto.Id == Guid.Empty)
                throw new ArgumentException("Customer id cannot be empty",
                    nameof(updateCustomerDto));
            Customer customer = updateCustomerDto.Adapt<Customer>();
            await _customerCommand.UpdateCustomer(customer);
        }
    }
}
