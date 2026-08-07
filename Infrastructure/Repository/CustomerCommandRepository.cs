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

        public Task UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
