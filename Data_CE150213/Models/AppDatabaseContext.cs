using Microsoft.EntityFrameworkCore;

namespace Data_CE150213.Models;

public class AppDatabaseContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Username = "nhatquynh1",
                Password = "123456",
                Fullname = "nhatquynh",
                Gender = "Nam",
                Birthday = new DateTime(2001, 9, 24),
                Address = "Cai Lay"
            },
            new Customer
            {
                Username = "nhatquynh2",
                Password = "123456",
                Fullname = "nhatquynh",
                Gender = "Nam",
                Birthday = new DateTime(2001, 9, 24),
                Address = "Cai Lay"
            },
            new Customer
            {
                Username = "nhatquynh3",
                Password = "123456",
                Fullname = "nhatquynh",
                Gender = "Nam",
                Birthday = new DateTime(2001, 9, 24),
                Address = "Cai Lay"
            }
        );
    }
}