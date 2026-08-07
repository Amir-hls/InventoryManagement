using Application.IServices;
using Domain.DTOs;
using Domain.Entities;
using Mapster;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerCommandRepository _customerCommand;
        public CustomerService(ICustomerCommandRepository customerCommand)
        {
            _customerCommand = customerCommand;
        }
        public async Task AddCustomer(AddCustomerDto addCustomerDto)
        {
            Customer newCustomer = addCustomerDto.Adapt<Customer>();
            await _customerCommand.InsertCustomer(newCustomer);
        }
    }
}
