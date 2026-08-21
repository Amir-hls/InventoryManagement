using Application.IRepository;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class CustomerCommandRepository(AppDbContext _dbContext) : ICustomerCommandRepository
    {

        public async Task<Guid> InsertCustomer(Customer customer)
        {
            await _dbContext.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer.Id;
        }

        public async Task UpdateCustomer(Customer customer)
        {  
            _dbContext.Update(customer);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> DeleteCustomer(Guid customerId) {
            var existingCustomer = await _dbContext.FindAsync<Customer>(customerId);
            if (existingCustomer == null)
                return false;
                
            _dbContext.Remove(existingCustomer);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Customer?> GetCustomerEntityAsync(Guid customerId)
        {
            var existingCustomer = await _dbContext.FindAsync<Customer>(customerId);
            return existingCustomer;
        }
    }
}
