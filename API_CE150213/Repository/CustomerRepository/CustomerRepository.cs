using Data_CE150213.Models;
using Data_CE150213.Repository.MSSQL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API_CE150213.Repository.CustomerRepository;

public class CustomerRepository : MSSQLRepository<AppDatabaseContext, Customer>, ICustomerRepository
{
    private readonly AppDatabaseContext DbContext;

    public CustomerRepository(AppDatabaseContext DbContext) : base(DbContext)
    {
        this.DbContext = DbContext;
    }

    public async Task<bool> AnyAsync(string username)
    {
        return await DbContext.Customers.AnyAsync(c => c.Username.Equals(username));
    }

    public async Task<Customer> GetCustomerAsync(string username)
    {
        return await DbContext.Customers.FirstOrDefaultAsync(c => c.Username.Equals(username));
    }

    public Task<int> GetCustomerCount()
    {
        return DbContext.Customers.CountAsync();
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        var customers = await DbContext.Customers.ToListAsync();
        return customers;
    }
}
