namespace Data_CE150213.Models;

public class DatabaseInitializer
{
    public static void Initialize(AppDatabaseContext DbContext)
    {

        var customers = new Customer[]
        {
            new Customer { Username = "nhatquynh4", Password = "123456", Fullname = "nhatquynh",
                            Gender = "Nam", Birthday = new DateTime(2001, 9, 24), Address = "Cai Lay" },
            new Customer { Username = "nhatquynh5", Password = "123456", Fullname = "nhatquynh",
                            Gender = "Nam", Birthday = new DateTime(2001, 9, 24), Address = "Cai Lay" },
            new Customer { Username = "nhatquynh6", Password = "123456", Fullname = "nhatquynh",
                            Gender = "Nam", Birthday = new DateTime(2001, 9, 24), Address = "Cai Lay" },
        };
        DbContext.Customers.AddRange(customers);
        DbContext.SaveChanges();
    }
}
