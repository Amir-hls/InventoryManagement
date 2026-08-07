using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface ICustomerService
    {
        Task AddCustomer(AddCustomerDto addCustomerDto);
    }
}
