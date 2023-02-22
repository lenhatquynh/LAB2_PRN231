using Data_CE150213.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Data_CE150213.Controllers;

public class CustomerController : Controller
{
    private readonly HttpClientHandler ClientHandler;
    public CustomerController()
    {
        ClientHandler = new HttpClientHandler();
        ClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
    }

    // GET: Customers
    public IActionResult Index()
    {
        HttpClient client = new HttpClient(ClientHandler);
        client.BaseAddress = new Uri("https://localhost:7111/odata/Customer/GetAllCustomers");
        var res = client.GetStringAsync(client.BaseAddress).GetAwaiter().GetResult();
        List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(res);
        return View(customers);
    }

    // post: Customer/create/LeNhatQuynh
    public async Task<IActionResult> Create([Bind("Username,Password,Fullname,Gender,Birthday,Address")] Customer customer)
    {
        HttpClient client = new HttpClient(ClientHandler);
        client.BaseAddress = new Uri($"https://localhost:7111/odata/Customer/CreateCustomer");
        var res = await client.PostAsJsonAsync(client.BaseAddress, customer);
        if (res.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return RedirectToAction(nameof(Index));
        }
        ViewData["Error"] = "Customer username is exist!";
        return View(customer);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // GET: Customer/Delete/LeNhatQuynh
    public IActionResult Delete(string id)
    {
        HttpClient client = new HttpClient(ClientHandler);
        client.BaseAddress = new Uri($"https://localhost:7111/odata/Customer/GetCustomerByUsername?Username={id}");
        var res = client.GetStringAsync(client.BaseAddress).GetAwaiter().GetResult();
        Customer customer = JsonConvert.DeserializeObject<Customer>(res);

        return View(customer);
    }

    // POST: Customer/Delete/LeNhatQuynh
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(string id)
    {
        HttpClient client = new HttpClient(ClientHandler);
        client.BaseAddress = new Uri($"https://localhost:7111/odata/Customer/DeleteCustomer?Username={id}");
        var res = client.DeleteAsync(client.BaseAddress).GetAwaiter().GetResult();
        return RedirectToAction(nameof(Index));
    }
}