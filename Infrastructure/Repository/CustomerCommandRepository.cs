using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository
{
    public class CustomerCommandRepository : ICustomerCommandRepository
    {
        private readonly AppDbContext _dbContext;
        public CustomerCommandRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertCustomer(Customer customer)
        {
            await _dbContext.AddAsync<Customer>(customer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCustomer(Customer customer)
        {
            _dbContext.Update<Customer>(customer);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteCustomer(Guid customerId) {
            var existingCustomer = await _dbContext.FindAsync<Customer>(customerId);
            if(existingCustomer != null)
            {
                _dbContext.Remove<Customer>(existingCustomer);
                await _dbContext.SaveChangesAsync();
            }
            
        }
    }
}
