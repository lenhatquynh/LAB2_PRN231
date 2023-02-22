using Data_CE150213.Models;
using Data_CE150213.Repository.MSSQL;

namespace API_CE150213.Repository.CustomerRepository;

public interface ICustomerRepository : IMSSQLRepository<Customer>
{
    Task<List<Customer>> GetCustomersAsync();

    Task<Customer> GetCustomerAsync(string username);

    Task<int> GetCustomerCount();
    Task<bool> AnyAsync(string username);
}
